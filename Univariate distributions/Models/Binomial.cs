using System.Runtime.CompilerServices;

namespace Univariate_distributions.Models
{
	public class Binomial : IDiscreteDistribution, IUnivariateDistribution, IDistribution
	{
		private System.Random _random;

		private readonly double _p;

		private readonly int _trials;

		/// <summary>
		/// Получает вероятность в каждом испытании. Диапазон: 0 ≤ p ≤ 1.
		/// </summary>
		public double P => _p;

		/// <summary>
		/// Получает количество испытаний. Диапазон: n ≥ 0.
		/// </summary>
		public int N => _trials;

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
		/// Получает математическое ожидаение распределения.
		/// </summary>
		public double Mean => _p * (double)_trials;

		/// <summary>
		/// Получает среднее квадратичное отклонение.
		/// </summary>
		public double StdDev => Math.Sqrt(_p * (1.0 - _p) * (double)_trials);
		
		/// <summary>
		/// Получает дисперсию распределения.
		/// </summary>
		public double Variance => _p * (1.0 - _p) * (double)_trials;

		/// <summary>
		/// Получает энтропию распределения.
		/// </summary>
		public double Entropy
		{
			get
			{
				if (_p == 0.0 || _p == 1.0)
				{
					return 0.0;
				}

				double num = 0.0;
				for (int i = 0; i <= _trials; i++)
				{
					double num2 = Probability(i);
					num -= num2 * Math.Log(num2);
				}

				return num;
			}
		}
  
		/// <summary>
		/// Получает асимметрию распределения.
		/// </summary>
		public double Skewness => (1.0 - 2.0 * _p) / Math.Sqrt((double)_trials * _p * (1.0 - _p));

		/// <summary>
		/// Получает наименьший элемент в области распределения, который может быть представлен
		/// целым числом.
		/// </summary>
		public int Minimum => 0;
  
		/// <summary>
		/// Получает самый большой элемент в области распределений, который может быть представлен
		/// целым числом.
		/// </summary>
		public int Maximum => _trials;

		/// <summary>
		/// Получает медиану распределения.
		/// </summary>
		public double Median => Math.Floor(_p * (double)_trials);

		/// <summary>
		/// Инициализирует новый экземпляр класса Binomial.
		/// </summary>
		/// <param name="p">Вероятность (p) в каждом испытании. Диапазон: 0 ≤ p ≤ 1.</param>
		/// <param name="n">Количество испытаний (n). Диапазон: n ≥ 0.</param>
		/// <exception cref="ArgumentException">Если p не находится в интервале [0.0,1.0] или n отрицательно.</exception>
		public Binomial(double p, int n)
		{
			if (!IsValidParameterSet(p, n))
			{
				throw new ArgumentException("Invalid parametrization for the distribution.");
			}

			_random = new Random();
			_p = p;
			_trials = n;
		}

		/// <summary>
		/// Инициализирует новый экземпляр класса Binomial.
		/// </summary>
		/// <param name="p">Вероятность (p) в каждом испытании. Диапазон: 0 ≤ p ≤ 1.</param>
		/// <param name="n">Количество испытаний (n). Диапазон: n ≥ 0.</param>
		/// <param name="randomSource">Генератор случайных чисел, используемый для получения случайных выборок.</param>
		/// <exception cref="ArgumentException">Если p не находится в интервале [0.0,1.0] или n отрицательно.</exception>
		public Binomial(double p, int n, System.Random randomSource)
		{
			if (!IsValidParameterSet(p, n))
			{
				throw new ArgumentException("Invalid parametrization for the distribution.");
			}

			_random = randomSource ?? new Random();
			_p = p;
			_trials = n;
		}
  
		/// <summary>
		/// Создаёт строковое представление выборки.
		/// </summary>
		/// <returns>строковое представление выборки.</returns>
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 2);
			defaultInterpolatedStringHandler.AppendLiteral("Binomial(p = ");
			defaultInterpolatedStringHandler.AppendFormatted(_p);
			defaultInterpolatedStringHandler.AppendLiteral(", n = ");
			defaultInterpolatedStringHandler.AppendFormatted(_trials);
			defaultInterpolatedStringHandler.AppendLiteral(")");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		/// <summary>
		/// Проверяет, являются ли предоставленные значения допустимыми параметрами этого распределения.
		/// </summary>
		/// <param name="p">Вероятность (p) в каждом испытании. Диапазон: 0 ≤ p ≤ 1.</param>
		/// <param name="n">Количество испытаний (n). Диапазон: n ≥ 0.</param>
		/// <returns>true - допустимые, false - не допустимые.</returns>
		public static bool IsValidParameterSet(double p, int n)
		{
			if (p >= 0.0 && p <= 1.0)
			{
				return n >= 0;
			}

			return false;
		}

		/// <summary>
		/// Вычисляет значение функции вероятности (PMF) в точке k, т.е. P(X = k).
		/// </summary>
		/// <param name="k">Оценочная точка.</param>
		/// <returns>вероятность (PMF) в точке k.</returns>
		public double Probability(int k)
		{
			return PMF(_p, _trials, k);
		}

		/// <summary>
		/// Вычисляет значение функции распределения (CDF) в точке x, т.е. P(X ≤ x).
		/// </summary>
		/// <param name="x">Оценочная точка.</param>
		/// <returns>распределение (CDF) в точке x.</returns>
		public double CumulativeDistribution(double x)
		{
			return CDF(_p, _trials, x);
		}

		/// <summary>
		/// Вычисляет значение функции вероятности (PMF) в точке k, т.е. P(X = k).
		/// </summary>
		/// <param name="p">Вероятность (p) в допустимом диапозоне. Диапозон: 0 ≤ p ≤ 1.</param>
		/// <param name="n">Количество испытаний (n). Диапозон: n ≥ 0.</param>
		/// <param name="k">Оценочная точка.</param>
		/// <returns>значение функции вероятности (PMF) в точке k</returns>
		/// <exception cref="ArgumentException">Если p не находится в интервале [0.0,1.0] или n отрицательно.</exception>
		public static double PMF(double p, int n, int k)
		{
			if (!IsValidParameterSet(p, n))
			{
				throw new ArgumentException("Invalid parametrization for the distribution.");
			}

			if (k < 0 || k > n)
			{
				return 0.0;
			}

			if (p == 0.0)
			{
				if (k != 0)
				{
					return 0.0;
				}

				return 1.0;
			}

			if (p == 1.0)
			{
				if (k != n)
				{
					return 0.0;
				}

				return 1.0;
			}

			return SpecialFunctions.GetBinCoeff(n, k) * Math.Pow(p, k) * Math.Pow(1.0 - p, n - k);
		}

		/// <summary>
		/// Вычисляет значение функции распределения (CDF) в точке x, т.е. P(X ≤ x).
		/// </summary>
		/// <param name="p">Вероятность (p) в допустимом диапозоне. Диапозон: 0 ≤ p ≤ 1.</param>
		/// <param name="n">Количество испытаний (n). Диапозон: n ≥ 0.</param>
		/// <param name="x">Оценочная точка.</param>
		/// <returns>значение функции распределения (CDF) в точке x.</returns>
		/// <exception cref="ArgumentException">Если p не находится в интервале [0.0,1.0] или n отрицательно.</exception>
		public static double CDF(double p, int n, double x)
		{
			if (!(p >= 0.0) || !(p <= 1.0) || n < 0)
			{
				throw new ArgumentException("Invalid parametrization for the distribution.");
			}

			if (x < 0.0)
			{
				return 0.0;
			}

			if (x > (double)n)
			{
				return 1.0;
			}

			double result = 0;
			for(int i = 0; i <= x; ++i)
			{
				result += PMF(p, n, (int)i);
			}

			return (result > 1) ? 1 : result;
		}
	}
}
