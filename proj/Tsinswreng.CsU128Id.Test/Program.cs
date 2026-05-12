using Microsoft.Extensions.DependencyInjection;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsU128Id.Test;

/// <summary>
/// This is the executable entrypoint of the test assembly.
/// It initializes the CsTreeTest manager and runs all registered tests.
/// </summary>
public static class Program{
	/// <summary>
	/// Launch all tests registered under <see cref="U128IdTestMgr"/>.
	/// </summary>
	/// <param name="Args">Command-line arguments (currently unused).</param>
	public static async Task Main(string[] Args){
		_ = Args;
		IServiceCollection SvcColct = new ServiceCollection();
		var Mgr = U128IdTestMgr.Inst;
		_ = Mgr.InitSvc(SvcColct, Sc => Sc.BuildServiceProvider());
		ITestExecutor Executor = new TreeTestExecutor();
		await Executor.RunEtPrint(Mgr.TestNode);
	}
}
