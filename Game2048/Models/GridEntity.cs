using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Models
{
    public class GridItem
    {
        internal GridHolder Owner { get; set; }

        private GridItem(GridHolder owner)
        {
            Owner = owner;
        }

        public int Value { get; private set; }

        // 等级
        public int Level
        {
            get { return (int)Math.Round(Math.Log(Value, 2)); }
        }

        private static readonly Random RANDOM = new Random();

        internal static GridItem GetRandomEntity(GridHolder owner)
        {
            return new GridItem(owner)
                {
                    Value = RANDOM.Next(1) == 1 ? 4 : 2
                };
        }

        public bool CanMerge(GridItem other)
        {
            return Value == other.Value;
        }

        public GridItem Merge(GridItem other)
        {
            if (Value != other.Value)
            {
                throw new Exception("GridItem Merge Error");
            }
            return new GridItem(null)
                {
                    Value = Value*2
                };
        }
    }
}
