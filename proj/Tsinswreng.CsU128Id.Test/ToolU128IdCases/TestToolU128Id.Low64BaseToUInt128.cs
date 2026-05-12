using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsU128Id.Test.ToolU128IdCases;

/// <summary>
/// Cases for <see cref="Tsinswreng.CsU128Id.ToolU128Id.Low64BaseToUInt128(string)"/>.
/// </summary>
public partial class TestToolU128Id{
	/// <summary>
	/// Register Low64BaseToUInt128 test cases.
	/// </summary>
	/// <param name="Node">Target node.</param>
	public void RegisterLow64BaseToUInt128(ITestNode Node){
		var Register = Node.MkTestFnRegister(
			typeof(TestToolU128Id),
			[typeof(Tsinswreng.CsU128Id.ToolU128Id)],
			[nameof(Tsinswreng.CsU128Id.ToolU128Id.Low64BaseToUInt128)],
			"Low64BaseToUInt128:"
		);
		var R = Register.Register;

		R("decodes-known-small-values", async _ => {
			AssertEqual((UInt128)0, Tsinswreng.CsU128Id.ToolU128Id.Low64BaseToUInt128("0"), "Low64BaseToUInt128/zero");
			AssertEqual((UInt128)1, Tsinswreng.CsU128Id.ToolU128Id.Low64BaseToUInt128("1"), "Low64BaseToUInt128/one");
			AssertEqual((UInt128)63, Tsinswreng.CsU128Id.ToolU128Id.Low64BaseToUInt128("_"), "Low64BaseToUInt128/sixty-three");
			AssertEqual((UInt128)64, Tsinswreng.CsU128Id.ToolU128Id.Low64BaseToUInt128("10"), "Low64BaseToUInt128/sixty-four");
			return null;
		});

		R("throws-argument-exception-for-invalid-ascii-character", async _ => {
			AssertThrows<ArgumentException>(
				() => Tsinswreng.CsU128Id.ToolU128Id.Low64BaseToUInt128("!"),
				"Low64BaseToUInt128/invalid-ascii"
			);
			return null;
		});

		R("throws-argument-exception-for-non-ascii-character", async _ => {
			AssertThrows<ArgumentException>(
				() => Tsinswreng.CsU128Id.ToolU128Id.Low64BaseToUInt128("ハ"),
				"Low64BaseToUInt128/non-ascii"
			);
			return null;
		});
	}
}
