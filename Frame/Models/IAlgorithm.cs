using MSAGL.Frame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAGL.Frame.Models
{
    public interface IAlgorithm
    {
        AlgorithmGraphType GraphType { get; }
        string Name { get; }
    }
}
