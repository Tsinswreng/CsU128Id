namespace Tsinswreng.CsUInt128Id;

public interface IU128Id{
	public UInt128 Value{get;set;}
}

/// 不依賴生成方式、生成方式在Extn中定義
public record struct U128Id:IU128Id{
	public U128Id(){}
	public U128Id(UInt128 V){
		this.Value = V;
	}
	public UInt128 Value{get;set;}
	public override string ToString() {
		return ToolU128Id.ToLow64Base(Value);
	}
}

public static partial class ToolU128Id{
	extension<T>(T z)
	where T:struct,IU128Id
	{
		public static T NewUlid(){
			var bytes = Ulid.NewUlid().ToByteArray();
			var u128 = ToolU128Id.ByteArrToUInt128(bytes);
			return new T(){Value = u128};
		}
		public str ToLow64Base(){
			return ToolU128Id.ToLow64Base(z.Value);
		}
		public u8[] ToByteArr(){
			return ToolU128Id.ToByteArr(z.Value);
		}
		public static T FromByteArr(byte[] bytes){
			var Num = ToolU128Id.ByteArrToUInt128(bytes);
			return new T(){Value = Num};
		}
	}
}
