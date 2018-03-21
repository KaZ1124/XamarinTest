using System;
namespace Calculation {
    public struct CalculatorNumber {
        private string ValString;

        public decimal Value{
            get{
                return decimal.Parse(ValString);
            }
        }

        private bool IsEmpty{
            get{
                return ValString == "-" || ValString == "";
            }
        }

        public CalculatorNumber(decimal val = 0m){
            ValString = val.ToString();
        }

        public CalculatorNumber(CalculatorNumber ops){
            ValString = ops.ValString;
        }

        public void PushNumber(int num){
            if (num < 0 || 9 < num)
                throw new ArgumentOutOfRangeException();

            ValString += num.ToString();
        }

        public void PushPeriod(){
            if (IsEmpty)
                ValString += "0";
            ValString += ".";
        }

        public void PushMinus(){
            if(ValString.Length == 0){
                ValString = "-";
            }else if(ValString[0] == '-'){
                ValString = ValString.Substring(1);
            }else{
                ValString = "-" + ValString;
            }
        }

        public void Clear(){
            ValString = "";
        }

        public void Delete(){
            ValString = ValString.Substring(0, ValString.Length - 1);
        }

        public override string ToString() {
            return Value.ToString();
        }
    }
}
