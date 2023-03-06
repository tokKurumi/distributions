using Univariate_distributions.Models;

class Program
{
	const double H = 2;

	static void Main(string[] args)
	{
		var binomial = new Binomial(0.2, 10);
		Console.WriteLine(binomial);
		for (int i = 0; i < binomial.N; ++i)
		{
			Console.WriteLine("binomial[{0}] = {1:N5} : {2:N5}", i, binomial.Probability(i), binomial.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var poisson = new Poisson(5);
		Console.WriteLine(poisson);
		for (int i = 0; i < 10; ++i)
		{
			Console.WriteLine("poisson[{0}] = {1:N5} : {2:N5}", i, poisson.Probability(i), poisson.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var uniform = new ContinuousUniform(0, 10);
		Console.WriteLine(uniform);
		for (double i = uniform.LowerBound, it = 0; i < uniform.UpperBound; i += H, ++it)
		{
			Console.WriteLine("uniform[{0}] = {1:N5} : {2:N5}", it, uniform.Density(i), uniform.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var exp = new Exponential(5);
		Console.WriteLine(exp);
		for (double i = 0, it = 0; i < 10; i += H, ++it)
		{
			Console.WriteLine("uniform[{0}] = {1:N5} : {2:N5}", it, exp.Density(i), exp.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var normal = new Normal();
		Console.WriteLine(normal);
		for (double i = 0, it = 0; i < 10; i += H, ++it)
		{
			Console.WriteLine("uniform[{0}] = {1:N5} : {2:N5}", it, normal.Density(i), normal.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var chisquared = new ChiSquared(5);
		Console.WriteLine(chisquared);
		for (double i = 0, it = 0; i < 10; i += H, ++it)
		{
			Console.WriteLine("chisquared[{0}] = {1:N5} : {2:N5}", it, chisquared.Density(i), chisquared.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var studentT = new StudentT();
		Console.WriteLine(studentT);
		for (double i = 0, it = 0; i < 10; i += H, ++it)
		{
			Console.WriteLine("chisquared[{0}] = {1:N5} : {2:N5}", it, studentT.Density(i), studentT.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var fisher = new FisherSnedecor(5, 10);
		Console.WriteLine(fisher);
		for (double i = 0, it = 0; i < 10; i += H, ++it)
		{
			Console.WriteLine("fisher[{0}] = {1:N5} : {2:N5}", it, fisher.Density(i), fisher.CumulativeDistribution(i));
		}
	}
}