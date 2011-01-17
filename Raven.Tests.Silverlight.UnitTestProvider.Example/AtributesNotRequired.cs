namespace Raven.Tests.Silverlight.UnitTestProvider.Example
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	public class AtributesNotRequired
	{
		public IEnumerable<Task> Execute_tests_without_attributes_that_return_tasks()
		{
			Assert.IsTrue(true);
			yield break;
		}

		public IEnumerable<Task> A_second_test()
		{
			Assert.IsTrue(true);
			yield break;
		}
	}
}