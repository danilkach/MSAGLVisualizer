using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using MSAGL.Forms;
using MSAGL.Frame.Algorithms;
using MSAGL.Frame.Models;

using MSAGLColor = Microsoft.Msagl.Drawing.Color;

namespace MSAGL
{
	public partial class Form1 : Form
	{
        #region Fields

        private Frame.Models.DGraph graph;
		private GViewer viewer;

		private Node firstClickedNode;

		private System.Drawing.Point newEdgeStart;
		private System.Drawing.Point newEdgeEnd;

		private CancellationTokenSource nodePickingCancellationTokenSource;
		private CancellationToken nodePickingCancellationToken;

		private List<Action> algorithms;

		private object selectedObject;
		private double defaultNodeFontSize = 12;
		private double defaultLineWidth = 1;
		private double defaultLabelFontSize = 8;
		private bool isExecutingAlgorithm = false;
        #endregion

        #region Initializers

        public Form1()
		{
			InitializeComponent();
			InitializeForm();
			InitializeAlgorithms();
			AdjustAlgorithms();
		}
        public void InitializeForm()
		{
			this.algorithms = new List<Action>();

			this.nodePickingCancellationTokenSource = new CancellationTokenSource();
			this.nodePickingCancellationToken = this.nodePickingCancellationTokenSource.Token;

			graph = new Frame.Models.DGraph(ArrowStyle.None);

			viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
			
			 viewer.EdgeInsertButtonVisible = viewer.UndoRedoButtonsVisible = 
				viewer.windowZoomButton.Visible = viewer.NavigationVisible = false;

			viewer.LayoutAlgorithmSettingsButtonVisible = true;

			graph.AddVertice("A");
			graph.AddVertice("B");
			graph.AddVertice("C");
			graph.AddVertice("D");

			//AddNodesLocations();

			graph.AddEdge("C", "B", 234);
			graph.AddEdge("A", "C", 53);
			graph.AddEdge("A", "C", 43);

			viewer.CenterToPoint(new Microsoft.Msagl.Core.Geometry.Point(viewer.Width / 2, viewer.Height / 2));
			viewer.Graph = graph;

            viewer.MouseDown += Viewer_MouseDown;
            viewer.MouseUp += Viewer_MouseUp;
            viewer.MouseMove += Viewer_MouseMove;
            viewer.MouseClick += Viewer_MouseClick;
            viewer.GraphLoadingEnded += Viewer_GraphLoadingEnded;
            viewer.GraphChanged += Viewer_GraphChanged;
            viewer.CustomOpenButtonPressed += Viewer_CustomOpenButtonPressed;
            viewer.KeyDown += Viewer_KeyDown;

            //viewer.ObjectUnderMouseCursorChanged += Viewer_ObjectUnderMouseCursorChanged;
			
			/*viewer.LayoutEditor.RemoveEdgeDraggingDecorations = null;
			viewer.LayoutEditor.RemoveObjDraggingDecorations = null;*/
			//viewer.AsyncLayout = true;

			this.SuspendLayout();
			viewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.graphPanel.Controls.Add(viewer);
			this.ResumeLayout();
		}
        private void InitializeAlgorithms()
        {
			AddAlgorithm(HamiltonianCycleAlgorithm.Instance, null, ls =>
			{
				var result = HamiltonianCycleAlgorithm.Instance.Execute(this.graph);
				Action action;
				if (result != null)
					action = () =>
					{
						this.textRepresentative.Text = "Гамильтонов цикл найден:\r\n";
						//this.textRepresentative.Text = "Цикл:\r\n";
						for(int i = 0; i < result.Item1.Count; i++)
							this.textRepresentative.Text += $"{result.Item1.ElementAt(i).Id} -> ";
						this.textRepresentative.Text += result.Item1.ElementAt(0).Id;
						ClearColorHighlighting();
						/*result.Item1.Take(result.Item1.Count - 1).All(node =>
						{
							this.textRepresentative.Text += $"{node.UserData.ToString()} -> ";
							return true;
						});*/
						HighlightCollection<Node>(result.Item1, MSAGLColor.Blue);
						HighlightCollection<Edge>(result.Item2, MSAGLColor.Blue);
					};
				else
					action = () =>
					{
						ClearColorHighlighting();
						this.textRepresentative.Text = "";
						this.textRepresentative.Text = "Гамильтонов цикл отсутствует";
					};
				Invoke(action);
			});
			AddAlgorithm(MinAndMaxPathToNodesAlgorithm.Instance, () =>
			{
				return new List<object> { PickNode("Выберите начальную вершину") };
			}, ls =>
			{
				var minResult = MinAndMaxPathToNodesAlgorithm.Instance.Execute(this.graph, ls.ElementAt(0) as Node);
				Action action = () =>
				{
					this.textRepresentative.Text = $"Минимальные расстояния от вершины {(ls.ElementAt(0) as Node).Id}:\r\n";
					if (minResult == null)
						this.textRepresentative.Text += "В графе присутствует отрицательный цикл\r\n";
					else
					{
						foreach (var pair in minResult)
							this.textRepresentative.Text += $"До {pair.Key.Id} = {pair.Value}\r\n";
					}
				};
				Invoke(action);
				var maxResult = MinAndMaxPathToNodesAlgorithm.Instance.Execute(this.graph, ls.ElementAt(0) as Node, false);
				Action maxAction = () =>
				{
					this.textRepresentative.Text += $"Максимальные расстояния от вершины {(ls.ElementAt(0) as Node).Id}:\r\n";
					if (maxResult == null)
						this.textRepresentative.Text += "В графе присутствует положительный цикл";
					else
					{
						foreach (var pair in maxResult)
							this.textRepresentative.Text += $"До {pair.Key.Id} = {pair.Value}\r\n";
					}
				};
				Invoke(maxAction);
			});
			AddAlgorithm(MaxFlowAlgorithm.Instance, () =>
			{
				return new List<object> { PickNode("Выберите исток"), PickNode("Выберите сток") };
			}, ls =>
			{
				if (ls[0] == ls[1])
					this.Invoke(new Action(
						() => this.textRepresentative.Text = "Одна и та же вершина не может выступать в качестве стока и истока одновременно"));
				else
				{
					var result = MaxFlowAlgorithm.Instance.Execute(this.graph,
						(ls[0] as Node), (ls[1] as Node));
					Action action = () =>
					{
						foreach (var pair in result)
							this.graph.Edges.First(e => e == pair.Key).LabelText =
								$"{pair.Value} / {this.graph.Edges.First(e => e == pair.Key).LabelText}";
						Node source = this.graph.Nodes.First(n => n == (ls[0] as Node));
						source.LabelText += " (исток)";
						//source.Label.FontSize -= 6;
						source.Attr.Color = MSAGLColor.Orange;
						Node sink = this.graph.Nodes.First(n => n == (ls[1] as Node));
						sink.LabelText += " (сток)";
						//sink.Label.FontSize -= 6;
						sink.Attr.Color = MSAGLColor.Orange;
						this.reloadGraph_Click(this.reloadGraph, null);
					};
					Invoke(action);
				}
			});
			AddAlgorithm(MaxCliqueAlgorithm.Instance, null, ls =>
			{
				Action action;
				if (this.graph.Edges.Count() == 0)
					action = () => { this.textRepresentative.Text = "Невозможно найти клику в пустом графе"; };
				else {
					var result = MaxCliqueAlgorithm.Instance.Execute(this.graph);
					action = () =>
					{
						this.textRepresentative.Text = "Найденные клики:\r\n\r\n";
						var sortedResult = result.OrderByDescending(r => r.Count);
						if (sortedResult.Count() > 0)
						{
							foreach (Node n in sortedResult.ElementAt(0))
							{
								n.Attr.Color = MSAGLColor.Blue;
								foreach (Node a in sortedResult.ElementAt(0))
									if (a != n)
										this.graph.Edges.First(e => (e.TargetNode == n && e.SourceNode == a)
										|| (e.TargetNode == a && e.SourceNode == n)).Attr.Color = MSAGLColor.Blue;
							}
							bool shownMax = false;
							int lastPower = int.MaxValue;
							foreach (List<Node> clique in sortedResult)
							{
								if (lastPower > clique.Count)
								{
									lastPower = clique.Count;
									this.textRepresentative.Text += $"Клики из {lastPower} вершин:\r\n";
								}
								for (int i = 0; i < clique.Count - 1; i++)
									this.textRepresentative.Text += $"{clique.ElementAt(i).UserData.ToString()} -> ";
								this.textRepresentative.Text += $"{clique.Last().UserData}";
								if(!shownMax)
                                {
									this.textRepresentative.Text += " - максимальная";
									shownMax = true;
                                }
								this.textRepresentative.Text += "\r\n";
							}
						}
						this.textRepresentative.Text += $"Клики из 2 вершин: каждое отдельное ребро графа";
					};
				}
				Invoke(action);
			});
			AddAlgorithm(ChromaticIndexAlgorithm.Instance, null, ls =>
			{
				Action action = new Action(() =>
				{
					this.textRepresentative.Text = "";
					var result = ChromaticIndexAlgorithm.Instance.Execute(this.graph);
					var sets = ChromaticIndexAlgorithm.Instance.FindIndependentSets(this.graph);
					this.textRepresentative.Text += "Независимые множества:\r\n";
					foreach(var set in sets)
                    {
						foreach (Node n in set)
							this.textRepresentative.Text += $"{n.UserData} ";
						this.textRepresentative.Text += "\r\n";
                    }
					this.textRepresentative.Text += $"Хроматический индекс = {result.Item1}\r\n";
					this.textRepresentative.Text += "Ребра и их цвета:\r\n";
					foreach (KeyValuePair<Edge, MSAGLColor> pair in result.Item2)
					{
						this.graph.Edges.First(e => e == pair.Key).Attr.Color = pair.Value;
						this.textRepresentative.Text +=
						$"{pair.Key.SourceNode.UserData} -> {pair.Key.TargetNode.UserData} ({pair.Value.ToString()})\r\n";
					}
				});
				Invoke(action);
			});
        }

		#endregion

		#region Utility Methods
		private void AddAlgorithm(IAlgorithm algorithm, 
		Func<List<Object>> pickingRule, Action<List<Object>> executionRule)
        {
			this.algorithms.Add(() => 
			{
				ChangeControlsState(false);
				this.clearColorHighlighting_Click(this.clearColorHighlighting, null);
				this.reloadGraph_Click(this.reloadGraph, null);
				this.nodePickingCancellationTokenSource = new CancellationTokenSource();
				this.nodePickingCancellationToken = this.nodePickingCancellationTokenSource.Token;
				this.isExecutingAlgorithm = true;
                Task.Factory.StartNew(() =>
				{
					this.viewer.LayoutEditingEnabled = false;
					try { executionRule(pickingRule == null ? null : pickingRule()); }
                    catch (Exception e) 
					{
						this.Invoke(new Action(() => {
							this.stateLabel.Text = "Ошибка при выполнении алгоритма";
							this.textRepresentative.Text = e.Message;
							this.isExecutingAlgorithm = false;
							ChangeControlsState(true); 
						}));
						this.viewer.LayoutEditingEnabled = true;
						return; 
					}
					finally {
						this.Invoke(new Action(() =>
						{
							this.isExecutingAlgorithm = false;
							this.stateLabel.Text = $"Алгоритм \"{algorithm.Name}\" {(this.nodePickingCancellationToken.IsCancellationRequested ? "отменен" : "выполнен")}";
						}));
						this.viewer.LayoutEditingEnabled = true;
					}
					this.Invoke(new Action(() => { ChangeControlsState(true); }));
				}, this.nodePickingCancellationToken);
			});
			this.algorithmDataGridView.Rows.Add();
			this.algorithmDataGridView.Rows[this.algorithmDataGridView.Rows.Count - 1].Cells[0].Value
				= algorithm.Name;
			this.algorithmDataGridView.Rows[this.algorithmDataGridView.Rows.Count - 1].Tag = algorithm.GraphType;
		}
		private void ChangeControlsState(bool state)
        {
			foreach (Control c in this.Controls)
				if (c != this.graphPanel && c != this.cancelButton && c != this.stateLabel)
					c.Enabled = state;
			this.cancelButton.Visible = !state;
			this.selectionGroupBox.Enabled = false;
		}
		public Node PickNode(string userMessage = "Выберите вершину")
        {
			this.selectedObject = null;
			Invoke(new Action(() => { this.stateLabel.Text = userMessage; }));
			return StartNodePickingListener();
        }
		private Node StartNodePickingListener()
        {
			while (!(this.selectedObject is Node))
				this.nodePickingCancellationToken.ThrowIfCancellationRequested();
			return this.selectedObject as Node;
        }
		private void HighlightEntity(Object entity, MSAGLColor color, bool reloadLayout = true)
        {
			if (entity is Edge)
				(entity as Edge).Attr.Color = color;
			else if (entity is Node)
				(entity as Node).Attr.Color = color;
			this.viewer.Invalidate();
        }
		private void HighlightCollection<Object>(IEnumerable<Object> collection, MSAGLColor color)
        {
			//FreezeGraphLayout();
			foreach (Object element in collection)
			{
				if (element is Node)
					HighlightEntity(this.graph.Nodes.First(n => n.UserData == (element as Node).UserData), color, false);
				else if (element is Edge)
					HighlightEntity(this.graph.Edges.First(e => e == (element as Edge)), color, false);
			}
			this.viewer.Invalidate();
			//ResumeGraphLayout();
        }
		private void ClearColorHighlighting()
        {
			this.selectedObject = null;
			this.selectionGroupBox.Enabled = false;
			//FreezeGraphLayout();
			foreach (Node n in this.graph.Nodes)
			{
				n.Label.FontSize = this.defaultNodeFontSize;
				n.Attr.Color = MSAGLColor.Black;
				n.LabelText = n.UserData.ToString();
				n.Attr.LineWidth = this.defaultLineWidth;
				this.viewer.ResizeNodeToLabel(n);
			}
			foreach (Edge edge in this.graph.Edges)
			{
				edge.Attr.Color = MSAGLColor.Black;
				edge.LabelText = double.Parse(edge.UserData.ToString()).ToString();
			}
			this.viewer.Invalidate();
			//ResumeGraphLayout();
		}
		private void ResetZoom()
        {
			this.viewer.Transform = null;
			this.viewer.DrawingPanel.Invalidate();
		}
		private void AdjustAlgorithms()
        {
			AlgorithmGraphType currentType = this.directedGraphRadioButton.Checked ? 
				AlgorithmGraphType.Directed : AlgorithmGraphType.Undirected;
			foreach (DataGridViewRow row in algorithmDataGridView.Rows)
				if ((row.Tag as AlgorithmGraphType?) == currentType)
				{
					(row.Cells[0] as DataGridViewTextBoxCell).Style.ForeColor =
					(row.Cells[1] as DataGridViewButtonCell).Style.ForeColor =
						System.Drawing.Color.Black;
				}
				else if((row.Tag as AlgorithmGraphType?) != AlgorithmGraphType.Any)
                {
					(row.Cells[0] as DataGridViewTextBoxCell).Style.ForeColor =
					(row.Cells[1] as DataGridViewButtonCell).Style.ForeColor = 
						System.Drawing.Color.LightGray;
				}
        }
		private void InsertEdge(Node n1, Node n2, object weight = null)
        {
			var edge = this.viewer.AddEdge(n1, n2, false, this.graph.EdgeArrowStyle);
			if (weight != null)
			{
				edge.UserData = weight;
				RelabelEdge(edge, weight.ToString());
				edge.Attr.Weight = int.Parse(weight.ToString());
			}
		}
		private void RelabelEdge(Edge e, string newLabel)
		{
			if (e.Label == null)
				e.Label = new Microsoft.Msagl.Drawing.Label(newLabel);
			else
				e.Label.Text = newLabel;

			e.Label.FontSize = this.defaultLabelFontSize;
			this.viewer.SetEdgeLabel(e, e.Label);
			e.Label.GeometryLabel.InnerPoints = new List<Microsoft.Msagl.Core.Geometry.Point>();
			var ep = new Microsoft.Msagl.Core.Layout.EdgeLabelPlacement(this.viewer.Graph.GeometryGraph);
			ep.Run();
			this.viewer.Graph.GeometryGraph.UpdateBoundingBox();
			this.viewer.LayoutEditor.AttachLayoutChangeEvent(this.viewer.Entities.First(
				entity => entity is DLabel && (entity as DLabel).DrawingLabel.Owner == e));
			this.viewer.Invalidate();
		}
		private Node InsertNode(Microsoft.Msagl.Core.Geometry.Point center, string id)
		{
			var node = new Node(id);
			node.UserData = node.Id = node.Label.Text = id;
			node.Attr.FillColor = MSAGLColor.Transparent;
			node.Label.FontColor = MSAGLColor.Black;
			node.Label.FontSize = this.defaultNodeFontSize;
			node.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
			IViewerNode dNode = this.viewer.CreateIViewerNode(node, center, null);
			this.viewer.AddNode(dNode, true);
			this.viewer.ResizeNodeToLabel(node);
			this.viewer.LayoutEditor.AttachLayoutChangeEvent(dNode);
			this.viewer.Invalidate(dNode);
			this.viewer.Refresh();
			return node;
		}

        #endregion

        #region Event Handlers
        //private void Viewer_ObjectUnderMouseCursorChanged(object sender, ObjectUnderMouseCursorChangedEventArgs e){}
        private void Viewer_CustomOpenButtonPressed(object sender, System.ComponentModel.HandledEventArgs e)
        {
			if (this.isExecutingAlgorithm)
			{
				MessageBox.Show("Перед загрузкой графа, завершите выполнение алгоритма. Код программы написан хорошо, но от таких маневров все упадет",
					"Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				e.Handled = true;
			}
        }
        private void Viewer_GraphChanged(object sender, EventArgs e)
        {
			ResetZoom();
        }
        private void Viewer_GraphLoadingEnded(object sender, EventArgs e)
        {
			ArrowStyle loadedGraphDirection = 
				(sender as GViewer).Graph.Edges.Last().Attr.ArrowheadAtTarget;
			this.graph = new Frame.Models.DGraph(this.graph.EdgeArrowStyle);
			/*if (this.graph.EdgeArrowStyle != loadedGraphDirection)
				this.graph.EdgeArrowStyle = loadedGraphDirection;*/
			if (loadedGraphDirection == ArrowStyle.None)
				this.undirectedGraphRadiobutton.Checked = true;
			else
				this.directedGraphRadioButton.Checked = true;
			foreach (Node node in (sender as GViewer).Graph.Nodes)
				this.graph.AddVertice(node.Id);
			foreach (Edge edge in (sender as GViewer).Graph.Edges)
				this.graph.AddEdge(edge.Source, edge.Target, edge.LabelText);
			this.viewer.Graph = null;
			this.viewer.NeedToCalculateLayout = true;
			this.viewer.Graph = this.graph;
			this.viewer.NeedToCalculateLayout = false;
			this.ResetZoom();
        }
        private void Viewer_MouseClick(object sender, MouseEventArgs e)
        {
			GViewer v = sender as GViewer;
			if(v.SelectedObject is Node)
            {
				Node n = v.SelectedObject as Node;
				this.selectionGroupBox.Text = $"Выбрана вершина {n.Id}";
				this.selectedEntityValueTextbox.Text = n.UserData.ToString();
				this.selectedObject = n;
				this.selectionGroupBox.Enabled = !this.isExecutingAlgorithm;
			} 
			else if(v.SelectedObject is Edge)
            {
				Edge edge = v.SelectedObject as Edge;
				this.selectionGroupBox.Text = $"Выбрано ребро {edge.ToString()}";
				this.selectedEntityValueTextbox.Text = edge.UserData.ToString();
				this.selectedObject = edge;
				this.selectionGroupBox.Enabled = !this.isExecutingAlgorithm;
			}
			else if(v.SelectedObject is Microsoft.Msagl.Drawing.Label)
            {
				Edge edge = (v.SelectedObject as Microsoft.Msagl.Drawing.Label).Owner as Edge;
				this.selectionGroupBox.Text = $"Выбрано ребро {edge.ToString()}";
				this.selectedEntityValueTextbox.Text = edge.UserData.ToString();
				this.selectedObject = edge;
				this.selectionGroupBox.Enabled = !this.isExecutingAlgorithm;
			}
			else
            {
				this.selectionGroupBox.Enabled = false;
				this.selectedObject = null;
			}
        }
        private void Viewer_MouseMove(object sender, MouseEventArgs e)
        {
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				ControlPaint.DrawReversibleLine(this.newEdgeStart, this.newEdgeEnd, System.Drawing.Color.Black);
				this.newEdgeEnd = Cursor.Position;
				ControlPaint.DrawReversibleLine(this.newEdgeStart, this.newEdgeEnd, System.Drawing.Color.Black);
			}
		}
        private void Viewer_MouseUp(object sender, MouseEventArgs e)
        {
			if (!this.isExecutingAlgorithm)
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					ControlPaint.DrawReversibleLine(this.newEdgeStart, this.newEdgeEnd, System.Drawing.Color.Black);
					GViewer v = sender as GViewer;
					var second = v.ObjectUnderMouseCursor;
					if (this.firstClickedNode != null && second != null && second is DNode)
					{
						var edgeForm = new EdgeCreationForm();
						if (edgeForm.ShowDialog() == DialogResult.OK)
						{
							this.InsertEdge(this.firstClickedNode, (second as DNode).Node, edgeForm.returnWeight);
							this.viewer.Refresh();
						}
					}
					this.firstClickedNode = null;
				}
			}
		}
        private void Viewer_MouseDown(object sender, MouseEventArgs e)
        {
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.newEdgeStart = this.newEdgeEnd = Cursor.Position;
				ControlPaint.DrawReversibleLine(this.newEdgeStart, this.newEdgeEnd, System.Drawing.Color.Black);
				GViewer v = sender as GViewer;
				if (v.SelectedObject is Node)
				{
					this.firstClickedNode = v.SelectedObject as Node;
                    System.Drawing.Point cursorPos = Cursor.Position;
					this.newEdgeStart = this.newEdgeEnd = cursorPos;
				}
				else
					this.firstClickedNode = null;
			}
        }
        private void Viewer_KeyDown(object sender, KeyEventArgs e)
        {
			if (this.selectedObject != null && e.KeyData == Keys.Delete)
				this.removeEntityButton_Click(this.removeEntityButton, null);
        }
        private void selectedEntityColor_Click(object sender, EventArgs e)
        {
			if(entityColorDialog.ShowDialog() == DialogResult.OK)
            {
				//FreezeGraphLayout();
				System.Drawing.Color c = entityColorDialog.Color;
				if (this.selectedObject is Node)
				{
					//(this.selectedObject as Node).Attr.LineWidth -= 1;
					(this.selectedObject as Node).Attr.Color = new MSAGLColor(c.A, c.R, c.G, c.B);
					viewer.Invalidate();
					//ResumeGraphLayout();
				}
				else if (this.selectedObject is Edge)
				{
					(this.selectedObject as Edge).Attr.Color = new MSAGLColor(c.A, c.R, c.G, c.B);
					//ResumeGraphLayout();
				}
				else if (this.selectedObject is Microsoft.Msagl.Drawing.Label)
					((this.selectedObject as Microsoft.Msagl.Drawing.Label).Owner as Edge).Attr.Color = new MSAGLColor(c.A, c.R, c.G, c.B);
				this.viewer.Invalidate();
				//this.reloadGraph_Click(this.reloadGraph, null);
			}
		}
        private void removeEntityButton_Click(object sender, EventArgs e)
        {
			if (this.selectedObject != null)
			{
				if (this.selectedObject is Node && this.graph.Nodes.Contains((this.selectedObject as Node)))
					this.viewer.RemoveNode(this.viewer.Entities.First(entity =>
					entity is DNode && (entity as DNode).Node == this.selectedObject as Node) as IViewerNode, false);
				//this.graph.RemoveNode(this.selectedObject as Node);
				else if (this.selectedObject is Edge && this.graph.Edges.Contains((this.selectedObject as Edge)))
	                //this.graph.RemoveEdge(this.selectedObject as Edge);
					this.viewer.RemoveEdge(this.viewer.Entities.First(entity =>
					entity is DEdge && (entity as DEdge).Edge == this.selectedObject as Edge) as IViewerEdge, false);
				this.selectionGroupBox.Enabled = false;
				this.selectedObject = null;
				this.viewer.Invalidate();
				this.viewer.SetSelectedObject(null);
				this.stateLabel.Text = $"Вершин: {this.graph.Nodes.Count()}; Ребер:{this.graph.Edges.Count()}";
			}
		}
        private void addNodeButton_Click(object sender, EventArgs e)
        {
			if (this.graph.Nodes.Any(n => n.UserData.ToString() == addNodeTextbox.Text))
				MessageBox.Show($"Будет тесновато для двух вершин с ID = {addNodeTextbox.Text}. Введите корректный ID вершины",
					"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else if(this.addNodeTextbox.Text.Length < 1)
				MessageBox.Show($"Два века назад даже каждый крепостной имел имя. Задайте ID вершины",
					"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else if(this.addNodeTextbox.Text.Count(c => c == ' ') == this.addNodeTextbox.Text.Length)
				MessageBox.Show($"ID из пробелов - это, конечно, хорошо,но,пожалуйста,задайтенормальноеимявершины",
					"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else if (addNodeTextbox.Text != null)
            {
				//viewer.AddEdge(this.graph.Nodes.ElementAt(0), this.graph.Nodes.ElementAt(1), false);
				if (this.graph.Nodes.Count() < 1)
				{
					this.graph.AddVertice(addNodeTextbox.Text);
					this.reloadGraph_Click(this.reloadGraph, null);
				}
				else
				{
					Node firstNode = this.graph.Nodes.ElementAt(0);
					this.InsertNode(new Microsoft.Msagl.Core.Geometry.Point(firstNode.Pos.X + firstNode.Width + 10, firstNode.Pos.Y), addNodeTextbox.Text);
				}
				addNodeTextbox.Text = null;
				this.addNodeTextbox.Focus();

            }
        }
        private void selectedEntityValueChangeButton_Click(object sender, EventArgs e)
        {
			if (selectedEntityValueTextbox.Text != null)
			{
				//FreezeGraphLayout();
				if (this.selectedObject is Node)
				{
					if (this.graph.Nodes.Any(n => n.Id == selectedEntityValueTextbox.Text))
						MessageBox.Show("Вершины может и не люди, но тоже должны отличаться. Введите корректный ID вершины",
							"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					else
					{
						Node n = this.graph.Nodes.First(n => n.UserData == (this.selectedObject as Node).UserData);
						object oldUserData = n.UserData;
						n.UserData = n.Id = n.LabelText = (this.selectedObject as Node).Id 
							= selectedEntityValueTextbox.Text;
						this.graph.nodeMap.Remove(oldUserData);
						this.graph.nodeMap.Add(n.UserData, n);

						this.viewer.ResizeNodeToLabel(n);
						DNode dNode = this.viewer.Entities.First(entity => entity is DNode &&
							(entity as DNode).Node.UserData == n.UserData) as DNode;
						this.viewer.DGraph.nodeMap.Remove(oldUserData.ToString());
						this.viewer.DGraph.nodeMap.Add(n.UserData.ToString(), dNode);
						foreach (Edge edge in this.graph.Edges)
							if (edge.Source == oldUserData.ToString())
								edge.Source = n.UserData.ToString();
							else if (edge.Target == oldUserData.ToString())
								edge.Target = n.UserData.ToString();
						foreach (Edge edge in n.Edges)
							this.viewer.Invalidate(this.viewer.Entities.First(entity => entity is DEdge && (entity as DEdge).Edge == edge));
						this.viewer.LayoutEditor.AttachLayoutChangeEvent(this.viewer.Entities.First(entity =>
							entity is DNode && (entity as DNode).Node == n));
						this.viewer.Invalidate();
						this.viewer.Refresh();
					}
				}
				else if (this.selectedObject is Edge)
				{
					double weight;
					if (double.TryParse(this.selectedEntityValueTextbox.Text, out weight))
					{
						Edge edge = (this.selectedObject as Edge);
						edge.UserData = edge.LabelText = selectedEntityValueTextbox.Text;
					}
					else
						MessageBox.Show("С невзвешенными графами программа пока работать не умеет. Введите корректный вес ребра",
							"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				/*if(this.selectedObject is Node)
					(this.selectedObject as Node).Attr.LineWidth -= 1;*/
				//ResumeGraphLayout();
				this.viewer.Invalidate();
				this.selectionGroupBox.Enabled = false;
				selectedEntityValueTextbox.Text = null;
			}
        }
        private void directedGraphRadioButton_CheckedChanged(object sender, EventArgs e)
        {
			if (this.directedGraphRadioButton.Checked && this.graph.EdgeArrowStyle == ArrowStyle.None)
			{
				if (this.graph.isEmpty())
					this.graph.EdgeArrowStyle = ArrowStyle.Normal;
				else if (MessageBox.Show("При смене типа графа, вся информация будет удалена. Продолжить?", "Внимание",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					this.graph.ClearGraph();
					this.graph.EdgeArrowStyle = ArrowStyle.Normal;
					this.selectionGroupBox.Enabled = false;
					this.reloadGraph_Click(this.reloadGraph, null);
				}
				else
					this.undirectedGraphRadiobutton.Checked = true;
				AdjustAlgorithms();
				/*else if (this.directedGraphRadioButton.Checked)
					this.undirectedGraphRadiobutton.Checked = true;*/
			}
        }
        private void undirectedGraphRadiobutton_CheckedChanged(object sender, EventArgs e)
        {
			if (this.undirectedGraphRadiobutton.Checked && this.graph.EdgeArrowStyle == ArrowStyle.Normal)
			{
				if (this.graph.isEmpty())
					this.graph.EdgeArrowStyle = ArrowStyle.None;
				else if (MessageBox.Show("При смене типа графа, вся информация будет удалена. Продолжить?", "Внимание",
					MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					this.graph.ClearGraph();
					this.graph.EdgeArrowStyle = ArrowStyle.None;
					this.selectionGroupBox.Enabled = false;
					this.reloadGraph_Click(this.reloadGraph, null);
				}
				else
					this.directedGraphRadioButton.Checked = true;
				AdjustAlgorithms();
				/*else if (this.undirectedGraphRadiobutton.Checked)
					this.directedGraphRadioButton.Checked = true;*/
			}
		}
        private void clearGraphButton_Click(object sender, EventArgs e)
        {
			if(!this.graph.isEmpty())
            {
				if (MessageBox.Show("Вся информация будет удалена. Продолжить?", "Внимание",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
					this.graph.ClearGraph();
					this.reloadGraph_Click(this.reloadGraph, null);
				}
			}
        }
        private void reloadGraph_Click(object sender, EventArgs e)
        {
			if (this.selectedObject != null && this.selectedObject is Node)
				(this.selectedObject as Node).Attr.LineWidth = this.defaultLineWidth;
			this.viewer.SetCalculatedLayout(this.viewer.CalculateLayout(this.graph));
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
			this.cancelButton.Visible = false;
			ChangeControlsState(true);
			this.nodePickingCancellationTokenSource.Cancel();
			this.isExecutingAlgorithm = false;
			this.selectedObject = null;
		}
        private void algorithmDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
			if (e.ColumnIndex == 1)
			{
				AlgorithmGraphType currentType = this.directedGraphRadioButton.Checked ?
				AlgorithmGraphType.Directed : AlgorithmGraphType.Undirected;
				if ((this.algorithmDataGridView.Rows[e.RowIndex].Tag as AlgorithmGraphType?) == currentType
					|| (this.algorithmDataGridView.Rows[e.RowIndex].Tag as AlgorithmGraphType?) == AlgorithmGraphType.Any)
					this.algorithms[e.RowIndex].Invoke();
				else
					MessageBox.Show($"Данный алгоритм применим к {(currentType == AlgorithmGraphType.Directed ? "неориентированному" : "ориентированному")} графу. По крайней мере в данной программе",
						"Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
        private void clearColorHighlighting_Click(object sender, EventArgs e)
        {
			ClearColorHighlighting();
        }
        private void clearTextInfoButton_Click(object sender, EventArgs e)
        {
			this.textRepresentative.Text = "";
		}
        private void addNodeTextbox_KeyDown(object sender, KeyEventArgs e)
        {
			if (e.KeyCode == Keys.Enter)
				this.addNodeButton_Click(this.addNodeButton, null);
		}
        private void selectedEntityValueTextbox_KeyDown(object sender, KeyEventArgs e)
        {
			if (e.KeyCode == Keys.Enter)
				this.selectedEntityValueChangeButton_Click(this.selectedEntityValueChangeButton, null);
        }

		#endregion

	}
}
