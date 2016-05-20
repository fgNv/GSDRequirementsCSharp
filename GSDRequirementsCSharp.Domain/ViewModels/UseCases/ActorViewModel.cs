using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class ActorViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<ActorContentViewModel> Contents { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public static ActorViewModel FromModel(Actor model)
        {
            return new ActorViewModel
            {
                Id = model.Id,
                Contents = model.Contents.Select(ActorContentViewModel.FromModel),
                X = model.X,
                Y = model.Y
            };
        }
    }
}
