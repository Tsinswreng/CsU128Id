using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsU128Id.Test.ToolU128IdCases;

/// <summary>
/// Main tester for <see cref="Tsinswreng.CsU128Id.U128IdExtn"/>.
/// Each partial file below registers one target method's cases.
/// </summary>
public partial class TestToolU128Id : ITester{
	/// <summary>
	/// Register all method-level test groups.
	/// </summary>
	/// <param name="Node">Optional node to register into.</param>
	/// <returns>The node after registration.</returns>
	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = false;
		Node.IsParallelRecursive = false;
		RegisterToLow64Base(Node);
		RegisterLow64BaseToUInt128(Node);
		RegisterTryLow64BaseToUInt128(Node);
		return Node;
	}

	/// <summary>
	/// Assert two values are equal.
	/// </summary>
	/// <typeparam name="T">Compared value type.</typeparam>
	/// <param name="Expected">Expected value.</param>
	/// <param name="Actual">Actual value.</param>
	/// <param name="CaseName">Readable case name for failure output.</param>
	private static void AssertEqual<T>(T Expected, T Actual, string CaseName){
		if(!EqualityComparer<T>.Default.Equals(Expected, Actual)){
			throw new Exception($"Case '{CaseName}' failed. Expected: {Expected}; Actual: {Actual}.");
		}
	}

	/// <summary>
	/// Assert the condition is true.
	/// </summary>
	/// <param name="Condition">Condition to verify.</param>
	/// <param name="CaseName">Readable case name for failure output.</param>
	private static void AssertTrue(bool Condition, string CaseName){
		if(!Condition){
			throw new Exception($"Case '{CaseName}' failed. Expected true but got false.");
		}
	}

	/// <summary>
	/// Assert the condition is false.
	/// </summary>
	/// <param name="Condition">Condition to verify.</param>
	/// <param name="CaseName">Readable case name for failure output.</param>
	private static void AssertFalse(bool Condition, string CaseName){
		if(Condition){
			throw new Exception($"Case '{CaseName}' failed. Expected false but got true.");
		}
	}

	/// <summary>
	/// Assert a specific exception type is thrown.
	/// </summary>
	/// <typeparam name="TException">Expected exception type.</typeparam>
	/// <param name="Act">Action under test.</param>
	/// <param name="CaseName">Readable case name for failure output.</param>
	private static void AssertThrows<TException>(Action Act, string CaseName)
		where TException : Exception{
		try{
			Act();
		}catch(TException){
			return;
		}catch(Exception Ex){
			throw new Exception(
				$"Case '{CaseName}' failed. Expected {typeof(TException).Name} but got {Ex.GetType().Name}.",
				Ex
			);
		}

		throw new Exception($"Case '{CaseName}' failed. Expected {typeof(TException).Name} but no exception was thrown.");
	}
}
