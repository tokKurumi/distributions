using Univariate_distributions.Models;

class Program
{
	static void Main(string[] args)
	{
		var binomial = new Binomial(0.7, 12);
		Console.WriteLine(binomial);
		for (int i = 0; i <= binomial.N; ++i)
		{
			Console.WriteLine("binomial[{0}] \t\t\t {1:N5} \t\t\t {2:N5}", i, binomial.Probability(i), binomial.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var poisson = new Poisson(8);
		Console.WriteLine(poisson);
		for (int i = 0; i <= 24; i += 2)
		{
			Console.WriteLine("poisson[{0}] \t\t\t {1:N5} \t\t\t {2:N5}", i, poisson.Probability(i), poisson.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var chisquared = new ChiSquared(3);
		Console.WriteLine(chisquared);
		for (double i = 0; i <= 24; i += 2)
		{
			Console.WriteLine("chisquared[{0}] \t\t\t {1:N5} \t\t\t {2:N5}", i, chisquared.Density(i), chisquared.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var exp = new Exponential(3);
		Console.WriteLine(exp);
		for (double i = 0; i <= 32; i += 4)
		{
			Console.WriteLine("exp[{0}] \t\t\t {1:N5} \t\t\t {2:N5}", i, exp.Density(i), exp.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var fisher = new FisherSnedecor(3, 5);
		Console.WriteLine(fisher);
		for (double i = 0; i <= 120; i += 10)
		{
			Console.WriteLine("fisher[{0}] \t\t\t {1:N5} \t\t\t {2:N5}", i, fisher.Density(i), fisher.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var normal = new Normal(5, 1);
		Console.WriteLine(normal);
		for (double i = 1; i <= 9; i += 0.5)
		{
			Console.WriteLine("normal[{0}] \t\t\t {1:N5} \t\t\t {2:N5}", i, normal.Density(i), normal.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var studentT = new StudentT(0, 1, 3);
		Console.WriteLine(studentT);
		for (double i = 0; i <= 30; i += 2)
		{
			Console.WriteLine("studentT[{0}] \t\t\t {1:N5} \t\t\t {2:N5}", i, studentT.Density(i), studentT.CumulativeDistribution(i));
		}

		Console.WriteLine();

		var uniform = new ContinuousUniform(3, 12);
		Console.WriteLine(uniform);
		for (double i = 0; i <= uniform.UpperBound; ++i)
		{
			Console.WriteLine("uniform[{0}] \t\t\t {1:N5} \t\t\t {2:N5}", i, uniform.Density(i), uniform.CumulativeDistribution(i));
		}
	}
}