using System;
using Xamarin.Forms;
using KaZXamarinLibrary.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Calculation {
    public class VerticalCalculationPage : ContentPage {
        /// <summary>
        /// 計算結果を表示するためのラベル
        /// </summary>
        protected Label LblAnswer;

        /// <summary>
        /// 数字ボタンを0〜9をインデックスに合わせて格納している配列
        /// </summary>
        protected Button[] BtnsNumber;

        /// <summary>
        /// 小数点（ピリオド）ボタン
        /// </summary>
        protected Button BtnPeriod;

        /// <summary>
        /// ACボタン
        /// </summary>
        protected Button BtnAllClear;

        /// <summary>
        /// C（クリアー）ボタン
        /// </summary>
        protected Button BtnClear;

        /// <summary>
        /// 一文字削除ボタン
        /// </summary>
        protected Button BtnDelete;

        /// <summary>
        /// 足し算ボタン
        /// </summary>
        protected Button BtnPlus;

        /// <summary>
        /// 引き算ボタン
        /// </summary>
        protected Button BtnMinus;

        /// <summary>
        /// 掛け算ボタン
        /// </summary>
        protected Button BtnMulti;

        /// <summary>
        /// 割り算ボタン
        /// </summary>
        protected Button BtnDivide;

        /// <summary>
        /// イコールボタン
        /// </summary>
        protected Button BtnEqual;

        /// <summary>
        /// レイアウトを初期化します。
        /// </summary>
        public VerticalCalculationPage() {
            InitMemberControl();
            Padding = DisplayPadding.Padding;
            Title = "電卓";

            var AnswerView = new Frame {
                Margin = new Thickness(10, 10, 10, 10),
                Padding = new Thickness(5, 0, 5, 2),
                BackgroundColor = Color.Cyan,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 100,
                Content = LblAnswer,
            };

            var ButtonView = new Grid {
                Margin = new Thickness(10, 0, 10, 10),
                RowSpacing = 5,
                ColumnSpacing = 5,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.Fill,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) },
                },
                ColumnDefinitions = {
                    new ColumnDefinition{Width = new GridLength(1.0, GridUnitType.Star)},
                    new ColumnDefinition{Width = new GridLength(1.0, GridUnitType.Star)},
                    new ColumnDefinition{Width = new GridLength(1.0, GridUnitType.Star)},
                    new ColumnDefinition{Width = new GridLength(1.0, GridUnitType.Star)},
                }
            };

            ButtonView.Children.Add(BtnAllClear, 0, 0);
            ButtonView.Children.Add(BtnClear, 1, 0);
            ButtonView.Children.Add(BtnDelete, 2, 0);

            foreach (var i in BtnsNumber.Skip(1).Select((v, ix) => new { val = v, ix = ix + 1 })) {
                if (i.ix == 0) throw new Exception();
                ButtonView.Children.Add(i.val, (i.ix - 1) % 3, 3 - (i.ix - 1) / 3);
            }

            ButtonView.Children.Add(BtnsNumber[0], 0, 2, 4, 5);
            ButtonView.Children.Add(BtnPeriod, 2, 4);

            ButtonView.Children.Add(BtnDivide, 3, 0);
            ButtonView.Children.Add(BtnMulti, 3, 1);
            ButtonView.Children.Add(BtnMinus, 3, 2);
            ButtonView.Children.Add(BtnPlus, 3, 3);
            ButtonView.Children.Add(BtnEqual, 3, 4);


            Content = new StackLayout {
                BackgroundColor = Color.Cyan,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Children = { AnswerView, ButtonView }
            };
        }

        /// <summary>
        /// メンバーのコントロールを全て初期化します
        /// </summary>
        protected void InitMemberControl() {
            LblAnswer = new Label {
                Text = "0",
                HorizontalTextAlignment = TextAlignment.End,
                VerticalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.FillAndExpand,
                FontSize = 40,
            };

            BtnsNumber = Enumerable.Range(0, 10).Select(v => new Button {
                Text = v.ToString(),
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Gray,
                TextColor = Color.White,
                FontSize = 40,
            }).ToArray();

            BtnPeriod = new Button {
                Text = ".",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Gray,
                TextColor = Color.White,
                FontSize = 40,
            };

            #region Clear関係のボタン
            Func<Button> CreateBtn1 = () => new Button {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.LightGray,
                TextColor = Color.Black,
                FontSize = 40,
            };

            BtnAllClear = CreateBtn1();
            BtnAllClear.Text = "AC";

            BtnClear = CreateBtn1();
            BtnClear.Text = "C";

            BtnDelete = CreateBtn1();
            BtnDelete.Text = "←";
            #endregion


            #region 演算関係のボタン
            Func<Button> CreateBtn2 = () => new Button {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Orange,
                TextColor = Color.White,
                FontSize = 40,
            };

            BtnPlus = CreateBtn2();
            BtnPlus.Text = "＋";

            BtnMinus = CreateBtn2();
            BtnMinus.Text = "−";

            BtnMulti = CreateBtn2();
            BtnMulti.Text = "×";

            BtnDivide = CreateBtn2();
            BtnDivide.Text = "÷";

            BtnEqual = CreateBtn2();
            BtnEqual.Text = "=";
            #endregion

            #region イベントハンドラーの設定
            foreach (var i in BtnCollectionClear)
                i.Clicked += BtnClears_Clicked;

            foreach (var i in BtnCollectionCalculate)
                i.Clicked += BtnCalculate_Clicked;

            foreach (var i in BtnCollectionNumber)
                i.Clicked += BtnNumbers_Clicked;
            #endregion
        }

        #region フォームを種類によってまとめて処理する場合に用いるプロパティ
        /// <summary>
        /// 消去関係のボタンを列挙
        /// </summary>
        /// <value>IEnumerable型で遅延評価で列挙</value>
        protected IEnumerable<Button> BtnCollectionClear{
            get{
                yield return BtnAllClear;
                yield return BtnClear;
                yield return BtnDelete;
            }
        }

        /// <summary>
        /// 演算関係のボタンを列挙
        /// </summary>
        /// <value>IEnumerable型で遅延評価で列挙</value>
        protected IEnumerable<Button> BtnCollectionCalculate{
            get{
                yield return BtnPlus;
                yield return BtnMinus;
                yield return BtnMulti;
                yield return BtnDivide;
                yield return BtnEqual;
            }
        }

        /// <summary>
        /// 数字関係（ピリオド含む）を列挙
        /// </summary>
        /// <value>IEnumerable型で遅延評価で列挙</value>
        protected IEnumerable<Button> BtnCollectionNumber{
            get{
                foreach (var i in BtnsNumber)
                    yield return i;

                yield return BtnPeriod;
            }
        }

        #endregion

        #region イベントハンドラー
        /// <summary>
        /// 消去関係のボタンが押された場合に呼ばれるイベントハンドラー
        /// </summary>
        /// <param name="sender">AC、C、Delのボタン</param>
        /// <param name="e"></param>
        protected void BtnClears_Clicked(object sender, EventArgs e){
            DisplayAlert("Test", sender.ToString(), "OK");
        }

        /// <summary>
        /// 演算子関係のボタンが押された場合に用いるイベントハンドラー
        /// </summary>
        /// <param name="sender">四則演算及びイコールのボタン</param>
        /// <param name="e"></param>
        protected void BtnCalculate_Clicked(object sender, EventArgs e){
            DisplayAlert("Test", sender.ToString(), "OK");
        }

        /// <summary>
        /// 数字関係のボタンが押された場合に呼ばれるイベントハンドラー
        /// </summary>
        /// <param name="sender">数字関係のボタン（ピリオド含める）</param>
        /// <param name="e">E.</param>
        protected void BtnNumbers_Clicked(object sender, EventArgs e){
            DisplayAlert("Test", sender.ToString(), "OK");    
        }

        #endregion

    }
}
