using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MauiEfCorePOC.ValueComparer
{
    public class ByteArrayValueComparer : ValueComparer<byte[]>
    {
        public ByteArrayValueComparer()
            : base(
                (b1, b2) => b1.SequenceEqual(b2),
                b => b.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                b => b.ToArray()) // Deep copy of byte array
        {
        }
    }
}

