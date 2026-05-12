using Tsinswreng.CsTreeTest;
using Tsinswreng.CsU128Id.Test.ToolU128IdCases;

namespace Tsinswreng.CsU128Id.Test;

/// <summary>
/// Test manager for this csproj.
/// It collects and registers all tester classes.
/// </summary>
public class U128IdTestMgr : DiEtTestMgr{
	public static U128IdTestMgr Inst = new();

	/// <summary>
	/// Register all testers into the root test node.
	/// </summary>
	/// <param name="Test">Optional input node. The root node is used in practice.</param>
	/// <returns>The root node after tester registration.</returns>
	public override ITestNode RegisterTestsInto(ITestNode? Test){
		Test = this.TestNode;
		this.RegisterTester<TestToolU128Id>();
		return Test;
	}
}
