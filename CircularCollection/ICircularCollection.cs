using System.Collections.Generic;

namespace CircularCollection
{
    public interface ICircularCollection<T> : IEnumerable<T>
    {
        T this[int index] { get; set; }
        void Rotate(int index);
        int Length { get; }
    }
}