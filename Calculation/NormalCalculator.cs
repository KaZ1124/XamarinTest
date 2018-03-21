using System;
namespace Calculation {
    public class NormalCalculator {
        /// <summary>
        /// 四則演算及びイコールを表す列挙体
        /// </summary>
        public enum Operators {
            None, Plus, Minus, Multi, Divide, Equal
        }

        #region データとそれに関するプロパティ
        private CalculatorNumber _Answer;
        private CalculatorNumber? _OpsValue;
        private Operators _Operator;

        protected CalculatorNumber Answer {
            get {
                return _Answer;
            }
            set {
                _Answer = value;
            }
        }

        protected CalculatorNumber? OpsValue {
            get {
                return _OpsValue;
            }
            set {
                _OpsValue = value;
            }
        }

        protected Operators Operator {
            get {
                return _Operator;
            }
            set {
                _Operator = value;
            }
        }

        /// <summary>
        /// 電卓表示部に表示するための文字列
        /// </summary>
        /// <value>表示するための文字列</value>
        public string VisibleString{
            get{
                if (OpsValue == null)
                    return Answer.ToString();
                else
                    return OpsValue.ToString();
            }
        }
        #endregion

        #region プロパティ
        /// <summary>
        /// OperatorがNoneであるかどうか調べる
        /// </summary>
        /// <value>Noneであればtrue</value>
        private bool IsNone {
            get {
                return Operator == Operators.None;
            }
        }

        /// <summary>
        /// BeforeNumberがnullでないか調べる
        /// </summary>
        /// <value>BeforeNumberがnullでなければtrue</value>
        private bool HasBefore {
            get {
                return OpsValue != null;
            }
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// Answer, OpsValue, Operatorの３つの値で初期化（3つとも省略可能）
        /// </summary>
        /// <param name="_ans">現在の答え</param>
        /// <param name="_opsValue">演算対象を入力中の一時保管場所</param>
        /// <param name="_operator">現在演算させようとしている処理の種類</param>
        public NormalCalculator(decimal _ans = 0, decimal? _opsValue = null, Operators _operator = Operators.None) {
            Answer = new CalculatorNumber(_ans);

            if (_opsValue == null)
                OpsValue = null;
            else
                OpsValue = new CalculatorNumber(_opsValue ?? 0);
            
            Operator = _operator;
        }

        public NormalCalculator(NormalCalculator ops) {
            Answer = ops.Answer;
            OpsValue = ops.OpsValue;
            Operator = ops.Operator;
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 数字を一つプッシュします。
        /// </summary>
        /// <param name="num">一桁の数字（一桁でない場合はArgumentOutOfRangeExceptionをスロー）</param>
        public void PushNumber(int num) {
            if (num < 0 || 9 < num) throw new ArgumentOutOfRangeException();

            if(Operator == Operators.None)
                Answer.PushNumber(num);
            else if(OpsValue is CalculatorNumber n){
                n.PushNumber(num);
            }else{
                OpsValue = new CalculatorNumber();
                OpsValue?.PushNumber(num);
            }
        }

        /// <summary>
        /// ピリオドをプッシュします。
        /// </summary>
        public void PushPeriod() {
            if (Operator == Operators.None)
                Answer.PushPeriod();
            else if (OpsValue is CalculatorNumber n) {
                n.PushPeriod();
            } else {
                OpsValue = new CalculatorNumber();
                OpsValue?.PushPeriod();
            }

        }

        /// <summary>
        /// 演算子をプッシュします。
        /// </summary>
        /// <param name="op">演算子の種類</param>
        public void PushOperator(Operators op){
            if (op == Operators.None) throw new ArgumentException();

            if (op == Operators.Equal) {
                CalAnswer();
                return;
            }

            if (OpsValue != null)
                CalAnswer();

            Operator = op;
        }

        /// <summary>
        /// オールクリア（AC）をプッシュします。
        /// </summary>
        public void PushAllClear(){
            Answer.Clear();
            OpsValue = null;
            Operator = Operators.None;
        }

        /// <summary>
        /// クリア（C）をプッシュします。
        /// </summary>
        public void PushClear(){
            switch(Operator){
            case Operators.None:
            case Operators.Equal:
                PushAllClear();
                break;
            default:
                OpsValue = new CalculatorNumber(0);
                break;
            }
        }

        /// <summary>
        /// 一文字削除をプッシュします。
        /// </summary>
        public void PushDelete(){
            switch(Operator){
            case Operators.None:
            case Operators.Equal:
                Answer.Delete();
                break;
            default:
                OpsValue?.Delete();
                break;
            }
        }

        /// <summary>
        /// 演算子に基づいて計算を実行します。
        /// </summary>
        protected void CalAnswer(){
            switch(Operator){
            case Operators.None:
            case Operators.Equal:
                break;
            case Operators.Plus:
                Answer = new CalculatorNumber(Answer.Value + (OpsValue ?? Answer).Value);
                break;
            case Operators.Minus:
                Answer = new CalculatorNumber(Answer.Value - (OpsValue ?? Answer).Value);
                break;
            case Operators.Multi:
                Answer = new CalculatorNumber(Answer.Value * (OpsValue ?? Answer).Value);
                break;
            case Operators.Divide:
                Answer = new CalculatorNumber(Answer.Value / (OpsValue ?? Answer).Value);
                break;
            }

            OpsValue = null;
            Operator = Operators.None;
        }
        #endregion
    }
}
