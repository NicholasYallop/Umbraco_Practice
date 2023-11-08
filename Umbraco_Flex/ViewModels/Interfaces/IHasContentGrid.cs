using Umbraco.Cms.Core.Models.Blocks;

namespace Umbraco_Flex.ViewModels.Interfaces;

public interface IHasContentGrid
{
    public abstract BlockGridModel? ContentGrid { get; }
}
