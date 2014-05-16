﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Models
{
    public class GridEntity
    {
        internal GridHolder Owner { get; set; }

        private GridEntity(GridHolder owner)
        {
            Owner = owner;
        }

        public int Value { get; set; }

        /// <summary>
        /// 是否进行了合并
        /// </summary>
        public bool IsMerged { get; set; }

        private static readonly Random RANDOM = new Random();

        internal static GridEntity GetRandomEntity(GridHolder owner)
        {
            return new GridEntity(owner)
                {
                    Value = RANDOM.Next(1) == 1 ? 4 : 2
                };
        }

        public bool TryMerge(GridEntity other)
        {
            if (Value == other.Value)
            {
                Value *= 2;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 被合并的
        /// </summary>
        public bool IsBeMerged { get; set; }

        /// <summary>
        /// 是否进行了移动
        /// </summary>
        public bool IsMoved { get; set; }
    }
}
