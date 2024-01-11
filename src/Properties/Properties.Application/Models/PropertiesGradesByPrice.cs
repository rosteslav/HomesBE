namespace BuildingMarket.Properties.Application.Models
{
    public class PropertiesGradesByPrice
    {
        private readonly Dictionary<int, int> _grades = new();
        private readonly decimal _priceHigherEnd;
        private decimal _lowestPrice;
        private decimal _highestPrice;
        private decimal _bestPrice;
        private int _bestPriceIndex = -1;

        public PropertiesGradesByPrice(decimal priceHigherEnd, IEnumerable<PropertyRedisModel> properties)
        {
            _priceHigherEnd = priceHigherEnd;

            if (!properties.Any() || priceHigherEnd <= 0)
                return;

            SetLowestPrice(properties);

            if (_priceHigherEnd >= _lowestPrice)
            {
                SetBestPrice(properties);
                SetGapsBelow(properties);
            }

            if (_bestPriceIndex < properties.Count() - 1)
                SetGapsAbove(properties);
        }

        public int GetPropertyGrade(int propertyId)
        {
            if (_priceHigherEnd == 0)
                return 10;

            if (_grades.TryGetValue(propertyId, out int grade))
                return grade;

            return 0;
        }

        private void SetLowestPrice(IEnumerable<PropertyRedisModel> properties)
        {
            var lowest = properties.First();
            _grades[lowest.Id] = 5;
            _lowestPrice = lowest.Price;
        }

        private void SetBestPrice(IEnumerable<PropertyRedisModel> properties)
        {
            var highest = properties.Last();
            _highestPrice = highest.Price;

            if (highest.Price <= _priceHigherEnd)
            {
                _grades[highest.Id] = 10;
                _bestPriceIndex = properties.Count() - 1;
                _bestPrice = highest.Price;
            }
            else
            {
                int bestPriceIndex = FindEqualOrCheaperPrice(
                    leftBoundary: 0,
                    rightBoundary: properties.Count() - 1,
                    priceTarget: _priceHigherEnd,
                    properties);

                if (bestPriceIndex != -1)
                {
                    var best = properties.ElementAt(bestPriceIndex);
                    _grades[best.Id] = 10;
                    _bestPriceIndex = bestPriceIndex;
                    _bestPrice = best.Price;
                }
            }
        }

        private void SetGapsBelow(IEnumerable<PropertyRedisModel> properties)
        {
            decimal priceRange = _bestPrice - _lowestPrice;
            decimal partPriceRange = priceRange / 4;

            if (priceRange > 0 && partPriceRange > 0)
            {
                int grade = 6;
                int leftBoundary = 0;
                int rightBoundary = _bestPriceIndex - 1;

                for (decimal maxPrice = _lowestPrice + partPriceRange; maxPrice <= _bestPrice; maxPrice += partPriceRange)
                {
                    int index = FindEqualOrCheaperPrice(leftBoundary, rightBoundary, priceTarget: maxPrice, properties);

                    if (index != -1)
                    {
                        var id = properties.ElementAt(index).Id;
                        _grades[id] = grade;
                        leftBoundary = index;
                    }

                    grade++;
                }
            }
        }

        private void SetGapsAbove(IEnumerable<PropertyRedisModel> properties)
        {
            decimal priceRange = _highestPrice - _priceHigherEnd;
            decimal partPriceRange = priceRange / 5;

            if (priceRange > 0 && partPriceRange > 0)
            {
                int grade = 1;
                int leftBoundary = _bestPriceIndex + 1;
                int rightBoundary = properties.Count() - 1;

                for (decimal maxPrice = _highestPrice - partPriceRange; maxPrice > _priceHigherEnd; maxPrice -= partPriceRange)
                {
                    var index = FindEqualOrCheaperPrice(leftBoundary, rightBoundary, maxPrice, properties);

                    if (index != -1)
                    {
                        var id = properties.ElementAt(index).Id;
                        _grades[id] = grade;
                        rightBoundary = index;
                    }

                    grade++;
                }
            }
        }

        private int FindEqualOrCheaperPrice(int leftBoundary, int rightBoundary, decimal priceTarget, IEnumerable<PropertyRedisModel> properties)
        {
            while (leftBoundary + 1 < rightBoundary)
            {
                int mid = (rightBoundary + leftBoundary) / 2;
                var midPrice = properties.ElementAt(mid).Price;
                var nextToMidPrice = properties.ElementAt(mid + 1).Price;

                if (midPrice <= priceTarget)
                {
                    if (nextToMidPrice > priceTarget)
                        return mid;
                    else
                        leftBoundary = mid;
                }
                else
                {
                    rightBoundary = mid;
                }
            }

            if (properties.ElementAt(rightBoundary).Price <= priceTarget)
                return rightBoundary;
            else if (properties.ElementAt(leftBoundary).Price <= priceTarget)
                return leftBoundary;

            return -1;
        }
    }
}
