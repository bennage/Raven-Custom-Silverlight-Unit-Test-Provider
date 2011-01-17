RavenDB Custom Silverlight Unit Test Provider
===========================================
An unit test provider for the Silverlight Unit Testing Framework that improves the syntax for asynchronous tests.

The Silverlight Testing Framework is built by Jeff Wilcox (http://www.jeff.wilcox.name/) and can be found in the Silverlight Toolkit (http://silverlight.codeplex.com/).

Specifically, this is implementation is a modification of default provider; that is VSTT or Visual Studio Team Test.

===========================================
This removes the need for all the of the EnqueueX methods on SilverlightTest, though they are in fact still used under the covers. In addition, this has a dependency on AsyncCtpLibrary_Silverlight.dll which replicates the System.Threading.Tasks namespace in Silverlight.

This project is only meant to help facilitate testing of the RavenDB client for Silverlight, however I felt that the general pattern might prove useful to others. O
The logic is pretty much the same as in the default VSTT provider except for in TestMethod.Invoke() and AsynchronousTaskTest. Look at these two if you want to reproduce the same effect in a different provider.