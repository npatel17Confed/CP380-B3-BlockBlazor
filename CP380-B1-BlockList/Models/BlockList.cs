﻿using System;
using System.Collections.Generic;

namespace CP380_B1_BlockList.Models
{
    public class BlockList
    {
        public IList<Block> Chain { get; set; }

        public int Difficulty { get; set; } = 2;

        public BlockList()
        {
            Chain = new List<Block>();
            MakeFirstBlock();
        }

        public void MakeFirstBlock()
        {
            var block = new Block(DateTime.Now, null, new List<Payload>());
            block.Mine(Difficulty);
            Chain.Add(block);
        }

        public void AddBlock(Block block)
        {
            // TODO
            block.PreviousHash = Chain[Chain.Count - 1].Hash;
            block.Mine(Difficulty);
            Chain.Add(block);
        }

        public bool IsValid()
        {
            // TODO
            bool valid = true;
            for (var i=1; i<Chain.Count; i++)
            {
                if (Chain[i].PreviousHash != Chain[i - 1].Hash || !Chain[i].Hash.StartsWith("CC"))
                {
                    valid = false;
                    break;
                }
            }
            return valid;
        }
    }
}
