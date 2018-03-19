using System;
using Xamarin.Forms;

namespace KaZXamarinLibrary.Forms{
    public static class DisplayPadding{
        /// <summary>
        /// ディスプレイの内側余白をデバイスに合わせて取得します。
        /// IOSは上側に20の余白を設ける場合などに使います。
        /// </summary>
        /// <value>余白情報の入ったThicknessの値</value>
        public static Thickness Padding{
            get{
                switch(Device.RuntimePlatform){
                case "iOS":
                    return new Thickness(0, 20, 0, 0);
                case "Android":
                    return new Thickness(0, 0, 0, 0);
                default:
                    throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// ディスプレイの上側の余白をデバイスに合わせて取得します。
        /// </summary>
        /// <value>ディスプレイの上側の余白</value>
        public static double Top{
            get{
                return Padding.Top;
            }
        }

        /// <summary>
        /// ディスプレイの右側の余白をデバイスに合わせて取得します。
        /// </summary>
        /// <value>ディスプレイの右側の余白</value>
        public static double Right{
            get{
                return Padding.Right;
            }
        }

        /// <summary>
        /// ディスプレイの下側の余白をデバイスに合わせて取得します。
        /// </summary>
        /// <value>ディスプレイの下側の余白</value>
        public static double Bottom{
            get{
                return Padding.Bottom;
            }
        }

        /// <summary>
        /// ディスプレイの左側の余白をデバイスに合わせて取得します。
        /// </summary>
        /// <value>ディスプレイの下側の余白</value>
        public static double Left{
            get{
                return Padding.Left;
            }
        }
    }
}
