using MathNet.Numerics;

namespace Univariate_distributions.Models
{
	public static class SpecialFunctions
	{
		/// <summary>
		/// Вычисляет количество сочетаний без повторений C(N, K).
		/// </summary>
		/// <param name="N">Общее количество элеметов.</param>
		/// <param name="K">Размер группы.</param>
		/// <returns>количество сочетаний без повторений C(N, K).</returns>
		/// <remarks>http://blog.plover.com/math/choose.html</remarks>
		public static long GetBinCoeff(long N, long K)
		{
			long r = 1;
			long d;
			if (K > N) return 0;
			for (d = 1; d <= K; d++)
			{
				r *= N--;
				r /= d;
			}
			return r;
		}

		/// <summary>
		/// Вычисляет факториал числа N.
		/// </summary>
		/// <param name="N">Фактор-число в диапозоне N >= 0.</param>
		/// <returns>факториал числа N.</returns>
		public static long Factorial(long N)
		{
			if(N == 0)
			{
				return 1;
			}

			long result = 1;
			for(long i = 1; i <= N; ++i)
			{
				result *= i;
			}

			return result;
		}

		/// <summary>
		/// Вычисляет значение функции Лапасса в точке a с заданной точностью p.
		/// </summary>
		/// <param name="a">Точка для оценки.</param>
		/// <param name="precision">Задаваемая точность.</param>
		/// <returns></returns>
		public static double Laplace(double a, double precision)
		{
			double result = 0;
			for (double i = 0; i < a; i += precision)
			{
				result += precision * Math.Abs(Math.Exp(-.5 * Math.Pow(i, 2)) + Math.Exp(-.5 * Math.Pow((i + precision), 2))) / 2.0;
			}
			result *= 1.0 / Math.Pow(2 * Math.PI, .5);
			return result;
		}

		/// <summary>
		/// Вычисляет значение функции ошибок в точке x.
		/// </summary>
		/// <param name="x">Оцениваемая точка.</param>
		/// <returns>значение функции ошибок в точке x.</returns>
		public static double Erf(double x)
		{
			// constants
			double a1 = 0.254829592;
			double a2 = -0.284496736;
			double a3 = 1.421413741;
			double a4 = -1.453152027;
			double a5 = 1.061405429;
			double p = 0.3275911;

			// Save the sign of x
			int sign = 1;
			if (x < 0)
				sign = -1;
			x = Math.Abs(x);

			// A&S formula 7.1.26
			double t = 1.0 / (1.0 + p * x);
			double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

			return sign * y;
		}
	}
}
