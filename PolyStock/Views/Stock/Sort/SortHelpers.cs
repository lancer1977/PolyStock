using System;
using DynamicData.Binding;
using PolyhydraGames.Extensions;

namespace PolyStock.Views.Stock
{
    public static class SortHelpers
    {
        public static Sort GetSort(this string command, Sort existingSort)
        {
            var existingSortString = existingSort.ToString();

            if (existingSortString.Contains(command))
            {
                return (command + (existingSortString.Contains("Ascending") ? "Descending" : "Ascending"))
                    .ToEnum<Sort>();
            }
            else
            {
                command = command + "Descending";
                var sort = command.ToEnum<Sort>();
                return sort;
            }
        }

        public static SortExpressionComparer<StockControlViewModel> GetSorter(this Sort x)
        {
            switch (x)
            {
                //case Sort.DeadAscending: return SortExpressionComparer<StockControlViewModel>.Ascending(vm => vm.Dead);
                //case Sort.DeadDescending: return SortExpressionComparer<StockControlViewModel>.Descending(vm => vm.Dead);
                //case Sort.TotalAscending: return SortExpressionComparer<StockControlViewModel>.Ascending(vm => vm.Current);
                //case Sort.TotalDescending: return SortExpressionComparer<StockControlViewModel>.Descending(vm => vm.Current);
                //case Sort.AlphabeticalAscending: return SortExpressionComparer<StockControlViewModel>.Ascending(vm => vm.County);
                //case Sort.AlphabeticalDescending: return SortExpressionComparer<StockControlViewModel>.Descending(vm => vm.County);
                //case Sort.MortalityAscending: return SortExpressionComparer<StockControlViewModel>.Ascending(vm => vm.MortalityRate);
                //case Sort.MortalityDescending: return SortExpressionComparer<StockControlViewModel>.Descending(vm => vm.MortalityRate);

                //case Sort.RiskAscending: return SortExpressionComparer<StockControlViewModel>.Ascending(vm => vm.CurrentRiskRate);
                //case Sort.RiskDescending: return SortExpressionComparer<StockControlViewModel>.Descending(vm => vm.CurrentRiskRate);

                //case Sort.ChangeAscending: return SortExpressionComparer<StockControlViewModel>.Ascending(vm => vm.CurrentChange);
                //case Sort.ChangeDescending: return SortExpressionComparer<StockControlViewModel>.Descending(vm => vm.CurrentChange);

                //case Sort.PercentAscending: return SortExpressionComparer<StockControlViewModel>.Ascending(vm => vm.CurrentChangeRate);
                //case Sort.PercentDescending: return SortExpressionComparer<StockControlViewModel>.Descending(vm => vm.CurrentChangeRate);

                //case Sort.PopulationAscending: return SortExpressionComparer<StockControlViewModel>.Ascending(vm => vm.Population);
                //case Sort.PopulationDescending: return SortExpressionComparer<StockControlViewModel>.Descending(vm => vm.Population);

                //case Sort.MaskUseAscending: return SortExpressionComparer<StockControlViewModel>.Ascending(vm => vm.MaskUse.Always);
                //case Sort.MaskUseDescending: return SortExpressionComparer<StockControlViewModel>.Descending(vm => vm.MaskUse.Always);

                default: throw new ArgumentOutOfRangeException(nameof(x), x, null);
            }
        }

        //public static string ToFriendlyString(this Sort x)
        //{
        //    switch (x)
        //    {
        //        case Sort.DeadAscending: return "Deaths Asc.";
        //        case Sort.DeadDescending: return "Deaths Desc.";
        //        case Sort.TotalAscending: return "Total Asc.";
        //        case Sort.TotalDescending: return "Total Desc.";
        //        case Sort.AlphabeticalAscending: return "Alpha Asc.";
        //        case Sort.AlphabeticalDescending: return "Alpha Desc.";
        //        case Sort.MortalityAscending: return "Mort Asc.";
        //        case Sort.MortalityDeccending: return "Mort Desc.";
        //        default:
        //            throw new ArgumentOutOfRangeException(nameof(x), x, null);
        //    }
        //}
    }
}