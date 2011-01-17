namespace Raven.Tests.Silverlight.UnitTestProvider
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Microsoft.Silverlight.Testing;
	using System.Threading.Tasks;

	public abstract class AsynchronousTaskTest : SilverlightTest
	{
		protected Task Delay(int milliseconds)
		{
			return TaskEx.Delay(TimeSpan.FromMilliseconds(milliseconds));
		}

		public void ExecuteTest(MethodInfo test)
		{
			var tasks = (IEnumerable<Task>)test.Invoke(this, new object[] { });
			IEnumerator<Task> enumerator = tasks.GetEnumerator();
			ExecuteTestStep(enumerator);
		}

		private void ExecuteTestStep(IEnumerator<Task> enumerator)
		{
			bool moveNextSucceeded = false;
			try
			{
				moveNextSucceeded = enumerator.MoveNext();
			}
			catch (Exception ex)
			{
				EnqueueTestComplete();
				return;
			}

			if (moveNextSucceeded)
			{
				try
				{
					Task next = enumerator.Current;
					EnqueueConditional(() => next.IsCompleted || next.IsFaulted);
					EnqueueCallback(() => ExecuteTestStep(enumerator));
				}
				catch (Exception ex)
				{
					EnqueueTestComplete();
					return;
				}
			}
			else EnqueueTestComplete();
		}
	}
}