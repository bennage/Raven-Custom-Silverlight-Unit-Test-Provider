namespace Raven.Tests.Silverlight.UnitTestProvider.Example
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Microsoft.Silverlight.Testing;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class Tests : AsynchronousTaskTest
	{
		[TestMethod]
		[Asynchronous]
		public void OldWay()
		{
			var something = SomeTestTask.DoSomethingAsync();
			EnqueueConditional((() => something.IsCompleted || something.IsFaulted));
			EnqueueCallback(() =>
			{
			    var another = SomeTestTask.DoSomethingAsync();
			    EnqueueConditional((() => another.IsCompleted || another.IsFaulted));
			    EnqueueCallback(() =>
				{
					EnqueueDelay(100);
					Assert.AreEqual(42, another.Result);
					EnqueueTestComplete();
				});
			});
		}

		[TestMethod]
		[Asynchronous]
		public IEnumerable<Task> NewWay()
		{
			var something = SomeTestTask.DoSomethingAsync();
			yield return something;

			var another = SomeTestTask.DoSomethingAsync();
			yield return another;

			yield return Delay(100);

			Assert.AreEqual(42, another.Result);
		}

        [TestMethod]
        [Asynchronous]
        public async Task AnotherWay()
        {
            await SomeTestTask.DoSomethingAsync();
            int result = await SomeTestTask.DoSomethingAsync();
            await Delay(100);

            Assert.AreEqual(42, result);
        }
	}

	public static class SomeTestTask
	{
		public static Task<int> DoSomethingAsync()
		{
			var tcs = new TaskCompletionSource<int>();
			tcs.TrySetResult(42);
			return tcs.Task;
		}
	}
}