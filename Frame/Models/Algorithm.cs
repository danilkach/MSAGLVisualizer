using System;

namespace MSAGL.Frame.Models
{
    public abstract class Algorithm<T> where T: Algorithm<T>
    {
        private static readonly Lazy<T> LazyInstance =
            new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);
        public static T Instance => LazyInstance.Value;
    }
}
