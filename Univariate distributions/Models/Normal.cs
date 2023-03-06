using System.Runtime.CompilerServices;
using Meta.Numerics.Functions;

namespace Univariate_distributions.Models
{
	public class Normal : IContinuousDistribution, IUnivariateDistribution, IDistribution
	{
		private System.Random _random;

		private readonly double _mean;

		private readonly double _stdDev;

		/// <summary>
		/// Получает математическое ожидаение (μ) распределения.
		/// </summary>
		public double Mean => _mean;

		/// <summary>
		/// Получает среднее квадратичное отклонение (σ). Диапозон: σ ≥ 0.
		/// </summary>
		public double StdDev => _stdDev;

		/// <summary>
		/// Получает дисперсию распределения.
		/// </summary>
		public double Variance => _stdDev * _stdDev;

		/// <summary>
		/// Получает точность нормального распределенияю
		/// </summary>
		public double Precision => 1.0 / (_stdDev * _stdDev);

		/// <summary>
		/// Получает или задает генератор случайных чисел, который используется для построения
		/// случайных выборок.
		/// </summary>
		public System.Random RandomSource
		{
			get
			{
				return _random;
			}
			set
			{
				_random = value ?? new Random();
			}
		}

		/// <summary>
		/// Получает энтропию распределения.
		/// </summary>
		public double Entropy => Math.Log(_stdDev) + 1.4189385332046727;

		/// <summary>
		/// Получает асимметрию распределения.
		/// </summary>
		public double Skewness => 0.0;

		/// <summary>
		/// Получает медиану распределения.
		/// </summary>
		public double Median => _mean;

		/// <summary>
		/// Получает наименьший элемент в области распределения, который может быть представлен
		/// целым числом.
		/// </summary>
		public double Minimum => double.NegativeInfinity;

		/// <summary>
		/// Получает самый большой элемент в области распределений, который может быть представлен
		/// целым числом.
		/// </summary>
		public double Maximum => double.PositiveInfinity;

		/// <summary>
		/// Инициализирует новый экземпляр класса Summary.
		/// mean = 0.0, stddev = 1.0.
		/// </summary>
		public Normal()
			: this(0.0, 1.0)
		{ }

		/// <summary>
		/// Инициализирует новый экземпляр класса Summary.
		/// mean = 0.0, stddev = 1.0.
		/// </summary>
		/// <param name="randomSource">Генератор случайных чисел.</param>
		public Normal(System.Random randomSource)
			: this(0.0, 1.0, randomSource)
		{ }

		/// <summary>
		/// Инициализирует новый экземпляр класса Summary.
		/// </summary>
		/// <param name="mean">Математическое ожидаение (μ) распределения.</param>
		/// <param name="stddev">Cреднее квадратичное отклонение (σ). Диапозон: σ ≥ 0.</param>
		/// <exception cref="ArgumentException">Если квадратичное отклонение (σ) < 0.</exception>
		public Normal(double mean, double stddev)
		{
			if (!IsValidParameterSet(mean, stddev))
			{
				throw new ArgumentException("Invalid parametrization for the distribution.");
			}

			_random = new Random();
			_mean = mean;
			_stdDev = stddev;
		}

		/// <summary>
		/// Инициализирует новый экземпляр класса Summary.
		/// </summary>
		/// <param name="mean">Математическое ожидаение (μ) распределения.</param>
		/// <param name="stddev">Cреднее квадратичное отклонение (σ). Диапозон: σ ≥ 0.</param>
		/// <param name="randomSource">Генератор случайных чисел.</param>
		/// <exception cref="ArgumentException">Если квадратичное отклонение (σ) < 0.</exception>
		public Normal(double mean, double stddev, System.Random randomSource)
		{
			if (!IsValidParameterSet(mean, stddev))
			{
				throw new ArgumentException("Invalid parametrization for the distribution.");
			}

			_random = randomSource ?? new Random();
			_mean = mean;
			_stdDev = stddev;
		}

		/// <summary>
		/// Создаёт строковое представление выборки.
		/// </summary>
		/// <returns>строковое представление выборки.</returns>
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 2);
			defaultInterpolatedStringHandler.AppendLiteral("Normal(Mu = ");
			defaultInterpolatedStringHandler.AppendFormatted(_mean);
			defaultInterpolatedStringHandler.AppendLiteral(", Sigma = ");
			defaultInterpolatedStringHandler.AppendFormatted(_stdDev);
			defaultInterpolatedStringHandler.AppendLiteral(")");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		/// <summary>
		/// Проверяет, являются ли предоставленные значения допустимыми параметрами этого распределения.
		/// </summary>
		/// <param name="mean">Математическое ожидаение (μ) распределения.</param>
		/// <param name="stddev">Cреднее квадратичное отклонение (σ). Диапозон: σ ≥ 0.</param>
		/// <returns>true - допустимые, false - не допустимые.</returns>
		public static bool IsValidParameterSet(double mean, double stddev)
		{
			if (stddev >= 0.0)
			{
				return !double.IsNaN(mean);
			}

			return false;
		}

		/// <summary>
		/// Вычисляет значение функции вероятности (PDF) в точке k, т.е. P(X = k).
		/// </summary>
		/// <param name="k">Оценочная точка.</param>
		/// <returns>вероятность (PMF) в точке k.</returns>
		public double Density(double k)
		{
			return PDF(_mean, _stdDev, k);
		}

		/// <summary>
		/// Вычисляет значение функции распределения (CDF) в точке x, т.е. P(X ≤ x).
		/// </summary>
		/// <param name="x">Оцениваемая точка.</param>
		/// <returns>значение функции распределения (CDF) в точке x.</returns>
		public double CumulativeDistribution(double x)
		{
			return CDF(_mean, _stdDev, x);
		}

		/// <summary>
		/// Вычисляет значение обратной функции распределения (CDF) в точке x, т.е. P(X ≤ x).
		/// </summary>
		/// <param name="p">Оцениваемая точка.</param>
		/// <returns>значение функции распределения (CDF) в точке x.</returns>
		/// <exception cref="ArgumentException">Если квадратичное отклонение (σ) < 0.</exception>
		/// <remarks>MATLAB: norminv</remarks>
		public double InverseCumulativeDistribution(double p)
		{
			return InvCDF(_mean, _stdDev, p);
		}

		/// <summary>
		/// Вычисляет значение функции плотности вероятности (PDF) в точке x, т.е. ∂P(X ≤ x)/∂x.
		/// </summary>
		/// <param name="mean">Математическое ожидаение (μ) распределения.</param>
		/// <param name="stddev">Среднее квадратичное отклонение (σ). Диапозон: σ ≥ 0.</param>
		/// <param name="x">Точка для оценки.</param>
		/// <returns>значение функции плотности вероятности (PDF) в точке x</returns>
		/// <exception cref="ArgumentException">Если квадратичное отклонение (σ) < 0.</exception>
		/// <remarks>MATLAB: normpdf</remarks>
		public static double PDF(double mean, double stddev, double x)
		{
			if (stddev < 0.0)
			{
				throw new ArgumentException("Invalid parametrization for the distribution.");
			}

			//double num = (k - _mean) / _stdDev;
			//return Math.Exp(-0.5 * num * num) / (2.5066282746310007 * _stdDev);

			return (1.0 / (stddev * Math.Sqrt(2 * Math.PI))) * Math.Exp(-((x - mean) * (x - mean) / (2 * stddev * stddev)));
		}

		/// <summary>
		/// Вычисляет значение функции распределения (CDF) в точке x, т.е. P(X ≤ x).
		/// </summary>
		/// <param name="mean">Математическое ожидаение (μ) распределения.</param>
		/// <param name="stddev">Среднее квадратичное отклонение (σ). Диапозон: σ ≥ 0.</param>
		/// <param name="x">Оцениваемая точка.</param>
		/// <returns>значение функции распределения (CDF) в точке x.</returns>
		/// <exception cref="ArgumentException">Если квадратичное отклонение (σ) < 0.</exception>
		/// <remarks>MATLAB: normcdf</remarks>
		public static double CDF(double mean, double stddev, double x)
		{
			if (stddev < 0.0)
			{
				throw new ArgumentException("Invalid parametrization for the distribution.");
			}

			return 0.5 * AdvancedMath.Erfc((mean - x) / (stddev * 1.4142135623730951));
		}

		/// <summary>
		/// Вычисляет значение обратной функции распределения (CDF) в точке x, т.е. P(X ≤ x).
		/// </summary>
		/// <param name="mean">Математическое ожидаение (μ) распределения.</param>
		/// <param name="stddev">Среднее квадратичное отклонение (σ). Диапозон: σ ≥ 0.</param>
		/// <param name="p">Оцениваемая точка.</param>
		/// <returns>значение функции распределения (CDF) в точке x.</returns>
		/// <exception cref="ArgumentException">Если квадратичное отклонение (σ) < 0.</exception>
		/// <remarks>MATLAB: norminv</remarks>
		public static double InvCDF(double mean, double stddev, double p)
		{
			if (stddev < 0.0)
			{
				throw new ArgumentException("Invalid parametrization for the distribution.");
			}

			return mean - stddev * 1.4142135623730951 * AdvancedMath.InverseErfc(2.0 * p);
		}
	}
}