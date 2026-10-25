namespace Tsinswreng.CsU128Id;

public interface IU128Id{
	public UInt128 Value{get;set;}
}

/// 不依賴生成方式、生成方式在Extn中定義
public partial record struct U128Id:IU128Id{
	public U128Id(){}
	public U128Id(UInt128 V){
		this.Value = V;
	}
	public UInt128 Value{get;set;}
	public override string ToString() {
		return U128Id.ToLow64Base(Value);
	}
}

