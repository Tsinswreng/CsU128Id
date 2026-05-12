using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsU128Id.Test.ToolU128IdCases;

/// <summary>
/// Cases for <see cref="Tsinswreng.CsU128Id.ToolU128Id.ToLow64Base(UInt128)"/>.
/// </summary>
public partial class TestToolU128Id{
	/// <summary>
	/// Register ToLow64Base test cases.
	/// </summary>
	/// <param name="Node">Target node.</param>
	public void RegisterToLow64Base(ITestNode Node){
		var Register = Node.MkTestFnRegister(
			typeof(TestToolU128Id),
			[typeof(Tsinswreng.CsU128Id.ToolU128Id)],
			[nameof(Tsinswreng.CsU128Id.ToolU128Id.ToLow64Base)],
			"ToLow64Base:"
		);
		var R = Register.Register;

		R("encodes-known-small-values", async _ => {
			AssertEqual("0", Tsinswreng.CsU128Id.ToolU128Id.ToLow64Base(0), "ToLow64Base/zero");
			AssertEqual("1", Tsinswreng.CsU128Id.ToolU128Id.ToLow64Base(1), "ToLow64Base/one");
			AssertEqual("_", Tsinswreng.CsU128Id.ToolU128Id.ToLow64Base(63), "ToLow64Base/sixty-three");
			AssertEqual("10", Tsinswreng.CsU128Id.ToolU128Id.ToLow64Base(64), "ToLow64Base/sixty-four");
			return null;
		});

		R("round-trips-large-values-via-parser", async _ => {
			var Cases = new[]{
				(UInt128)0,
				(UInt128)1,
				(UInt128)64,
				(UInt128)ulong.MaxValue,
				UInt128.MaxValue
			};
			foreach(var Value in Cases){
				var Encoded = Tsinswreng.CsU128Id.ToolU128Id.ToLow64Base(Value);
				var Decoded = Tsinswreng.CsU128Id.ToolU128Id.Low64BaseToUInt128(Encoded);
				AssertEqual(Value, Decoded, $"ToLow64Base/round-trip/{Value}");
			}
			return null;
		});
	}
}
