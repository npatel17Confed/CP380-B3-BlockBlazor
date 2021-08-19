using CP380_B1_BlockList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP380_B3_BlockBlazor.Data
{
    class MiningService
    {
        private readonly BlockService _blockService;
        private readonly PendingTransactionService _pendingTransactionService;
        private IEnumerable<Block> blocks { get; set; }
        private IEnumerable<Payload> pendingPayloads { get; set; }

        public MiningService(BlockService blockService, PendingTransactionService pendingTransactionService)
        {
            _blockService = blockService;
            _pendingTransactionService = pendingTransactionService;
        }

        public async Task<Block> MineBlock()
        {
            blocks = await _blockService.GetBlocksAsync();
            pendingPayloads = await _pendingTransactionService.GetPayloadsAsync();

            Block block = new Block(DateTime.Now, blocks.Select(row => row.PreviousHash).Last(), pendingPayloads.ToList());
            block.Mine(2);

            return block;
        }
    }
}