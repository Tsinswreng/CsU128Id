using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsU128Id.Test.ToolU128IdCases;

/// <summary>
/// Cases for <see cref="Tsinswreng.CsU128Id.U128IdExtn.TryLow64BaseToUInt128(string, out UInt128)"/>.
/// </summary>
public partial class TestToolU128Id{
	/// <summary>
	/// Register TryLow64BaseToUInt128 test cases.
	/// </summary>
	/// <param name="Node">Target node.</param>
	public void RegisterTryLow64BaseToUInt128(ITestNode Node){
		var Register = Node.MkTestFnRegister(
			typeof(TestToolU128Id),
			[typeof(Tsinswreng.CsU128Id.U128IdExtn)],
			[nameof(Tsinswreng.CsU128Id.U128IdExtn.TryLow64BaseToUInt128)],
			"TryLow64BaseToUInt128:"
		);
		var R = Register.Register;

		R("returns-true-and-value-for-valid-input", async _ => {
			var Ok = Tsinswreng.CsU128Id.U128IdExtn.TryLow64BaseToUInt128("10", out var Value);
			AssertTrue(Ok, "TryLow64BaseToUInt128/valid/ok");
			AssertEqual((UInt128)64, Value, "TryLow64BaseToUInt128/valid/value");
			return null;
		});

		R("returns-false-for-invalid-ascii-character", async _ => {
			var Ok = Tsinswreng.CsU128Id.U128IdExtn.TryLow64BaseToUInt128("!", out var Value);
			AssertFalse(Ok, "TryLow64BaseToUInt128/invalid-ascii/ok");
			AssertEqual(default(UInt128), Value, "TryLow64BaseToUInt128/invalid-ascii/value");
			return null;
		});

		R("returns-false-for-non-ascii-character-without-throwing", async _ => {
			var Ok = Tsinswreng.CsU128Id.U128IdExtn.TryLow64BaseToUInt128("ハ", out var Value);
			AssertFalse(Ok, "TryLow64BaseToUInt128/non-ascii/ok");
			AssertEqual(default(UInt128), Value, "TryLow64BaseToUInt128/non-ascii/value");
			return null;
		});
	}
}
