namespace Univariate_distributions.Models
{
	public interface IDistribution
	{
		/// <summary>
		/// Получает или принимает генератор случайных чисел выборки.
		/// </summary>
		System.Random RandomSource { get; set; }
	}
}