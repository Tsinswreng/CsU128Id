using System.Buffers.Binary;

namespace Tsinswreng.CsU128Id;
public static partial class U128IdExtn{
	public static u8[] ToByteArr(this UInt128 value){
		var R = new u8[16];
		var span = R.AsSpan();
		ToByteSpan(value, ref span);
		return R;
	}


	/// 高位在前

	public static void ToByteSpan(
		this UInt128 value
		,ref Span<u8> R
	){
		// 拆分为高64位（upper）和低64位（lower）
		ulong upper = (ulong)(value >> 64);
		ulong lower = (ulong)value;

		// 大端序写入：先upper后lower
		BinaryPrimitives.WriteUInt64BigEndian(R.Slice(0, 8), upper);  // 高64位在前
		BinaryPrimitives.WriteUInt64BigEndian(R.Slice(8, 8), lower);  // 高64位在前8, 8), lower);  // 低64位在后
	}


	/// 高位在前

	public static Span<u8> ToByteSpan(
		this UInt128 value
	){
		Span<u8> R = new u8[16];
		ToByteSpan(value, ref R);
		return R;
	}
	extension<T>(T z)
	where T:struct,IU128Id
	{
		public static T NewUlid(){
			var bytes = Ulid.NewUlid().ToByteArray();
			var u128 = U128Id.ByteArrToUInt128(bytes);
			return new T(){Value = u128};
		}
		public str ToLow64Base(){
			return U128Id.ToLow64Base(z.Value);
		}
		public u8[] ToByteArr(){
			return U128IdExtn.ToByteArr(z.Value);
		}
		public static T FromByteArr(byte[] bytes){
			var Num = U128Id.ByteArrToUInt128(bytes);
			return new T(){Value = Num};
		}
	}
}
