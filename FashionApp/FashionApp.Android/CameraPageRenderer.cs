using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Com.Googlecode.Tesseract.Android;
using Android.Support.V4.View;

using FashionApp;
using FashionApp.Droid;
using FashionApp.Views;
using Android.Hardware;
using Android.Graphics;


[assembly: ExportRenderer(typeof(ProfilePage), typeof(CameraPageRenderer))]

namespace FashionApp.Droid
{
    public class CameraPageRenderer : PageRenderer, TextureView.ISurfaceTextureListener, Android.Views.View.IOnTouchListener
    {
        global::Android.Hardware.Camera camera;
        global::Android.Widget.Button takePhotoButton;
        global::Android.Widget.Button toggleFlashButton;
        global::Android.Widget.Button switchCameraButton;
        global::Android.Views.View view;

        Activity activity;
        CameraFacing cameraType;
        TextureView textureView;
        SurfaceView surfaceView;
        SurfaceTexture surfaceTexture;
        TextView OCR_textView;
        ImageView OCR_Rectangle;
        ImageView OCR_Top_Left;
        ImageView OCR_Top_Right;
        ImageView OCR_Bottom_Left;
        ImageView OCR_Bottom_Right;

        string filePath;

        int centerX;
        int centerY;

        int lastX;
        int lastY;

        bool flag = false;

        public CameraPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                SetupUserInterface();
                SetupEventHandlers();
                AddView(view);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"			ERROR: ", ex.Message);
            }

            try
            {
                this.SetOnTouchListener(this);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"			ERROR: ", ex.Message);
            }

        }

        public void onDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            OCR_textView.Text = "OCR : move ";

        }

        public bool OnTouch(Android.Views.View view, MotionEvent e)
        {

            if (camera != null)
            {
                switch(e.Action) {
                    case MotionEventActions.Down:
                        //OCR_textView.Text = "OCR : down ";
                        lastX = (int)e.GetX();
                        lastY = (int)e.GetY();

                        flag = !flag;
                        if (flag)
                        {
                            //OCR_Rectangle.SetX(e.GetX());
                            //OCR_Rectangle.SetY(e.GetY());
                        }
                        else
                        {
                            if ((int)e.GetX() - lastX > 0)
                            {
                                //OCR_Rectangle.LayoutParameters.Width = (int)e.GetX() - lastX;
                            }
                            if ((int)e.GetY() - lastY > 0)
                            {
                                //OCR_Rectangle.LayoutParameters.Height = (int)e.GetY() - lastY;
                            }
                            //OCR_Rectangle.RequestLayout();

                        }

                        break;
                    case MotionEventActions.Move:

                        int deltaX = (int)e.GetX() - lastX;
                        int deltaY = (int)e.GetY() - lastY;

                        centerX = (int)OCR_Rectangle.GetX() + OCR_Rectangle.Width / 2;
                        centerY = (int)OCR_Rectangle.GetY() + OCR_Rectangle.Height / 2;

                        if (((int)e.GetX() >= centerX) && ((int)e.GetY() >= centerY))
                        {
                            OCR_Rectangle.SetX(OCR_Rectangle.GetX() - deltaX );
                            OCR_Rectangle.SetY(OCR_Rectangle.GetY() - deltaY * 2);
                            OCR_Rectangle.LayoutParameters.Width += deltaX * 2;
                            OCR_Rectangle.LayoutParameters.Height += deltaY * 4;

                            OCR_Top_Left.SetX(OCR_Rectangle.GetX());
                            OCR_Top_Left.SetY(OCR_Rectangle.GetY());
                            OCR_Top_Right.SetX(OCR_Rectangle.GetX() + OCR_Rectangle.Width - OCR_Top_Right.Width);
                            OCR_Top_Right.SetY(OCR_Rectangle.GetY());
                            OCR_Bottom_Left.SetX(OCR_Rectangle.GetX());
                            OCR_Bottom_Left.SetY(OCR_Rectangle.GetY() + OCR_Rectangle.Height - OCR_Bottom_Left.Height);
                            OCR_Bottom_Right.SetX(OCR_Rectangle.GetX() + OCR_Rectangle.Width - OCR_Bottom_Right.Width);
                            OCR_Bottom_Right.SetY(OCR_Rectangle.GetY() + OCR_Rectangle.Height - OCR_Bottom_Right.Height);
                        }


                        OCR_Rectangle.RequestLayout();


                        lastX = (int)e.GetX();
                        lastY = (int)e.GetY();

                        break;
                    case MotionEventActions.Up:

                        break;
                    default:
                        break;
                }
            }

            return true;
        }

        void SetupUserInterface()
        {
            activity = this.Context as Activity;
            view = activity.LayoutInflater.Inflate(Resource.Layout.CameraLayout, this, false);
            cameraType = CameraFacing.Back;

            textureView = view.FindViewById<TextureView>(Resource.Id.textureView);
            textureView.SurfaceTextureListener = this;

            OCR_textView = view.FindViewById<TextView>(Resource.Id.myOCR_TextView);
            OCR_Rectangle = view.FindViewById<ImageView>(Resource.Id.btn_capture_photo);
            OCR_Top_Left = view.FindViewById<ImageView>(Resource.Id.Top_Left_photo);
            OCR_Top_Right = view.FindViewById<ImageView>(Resource.Id.Top_Right_photo);
            OCR_Bottom_Left = view.FindViewById<ImageView>(Resource.Id.Bottom_Left_photo);
            OCR_Bottom_Right = view.FindViewById<ImageView>(Resource.Id.Bottom_Right_photo);

            Canvas canvas = textureView.LockCanvas();
            Paint paint;
            paint = new Paint();
            paint.Color = Android.Graphics.Color.Red;
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = 2f;
            Rect r = new Rect((int)100, (int)100, (int)200, (int)200);
            // canvas.DrawRect(r, paint);



            var absolutePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).AbsolutePath;
            if (!Directory.Exists(absolutePath + "/tessdata"))
                Directory.CreateDirectory(absolutePath + "/tessdata");
            if (!File.Exists(absolutePath + "/tessdata/eng.traineddata"))
            {
                string destPath = absolutePath + "/tessdata/eng.traineddata";
                using (var source = activity.Assets.Open("eng.traineddata"))
                using (var dest = File.OpenWrite(destPath))
                    source.CopyTo(dest);
            }

        }

        void SetupEventHandlers()
        {
            takePhotoButton = view.FindViewById<global::Android.Widget.Button>(Resource.Id.takePhotoButton);
            takePhotoButton.Click += TakePhotoButtonTapped;

            switchCameraButton = view.FindViewById<global::Android.Widget.Button>(Resource.Id.switchCameraButton);
            switchCameraButton.Click += SwitchCameraButtonTapped;

        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

            view.Measure(msw, msh);
            view.Layout(0, 0, r - l, b - t);
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {

        }

        public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
            camera = global::Android.Hardware.Camera.Open((int)cameraType);
            textureView.LayoutParameters = new FrameLayout.LayoutParams(width, height);
            surfaceTexture = surface;

            camera.SetPreviewTexture(surface);
            PrepareAndStartCamera();
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
        {
            camera.StopPreview();
            camera.Release();
            return true;
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
            PrepareAndStartCamera();
        }

        void PrepareAndStartCamera()
        {
            camera.StopPreview();

            var display = activity.WindowManager.DefaultDisplay;
            if (display.Rotation == SurfaceOrientation.Rotation0)
            {
                camera.SetDisplayOrientation(90);
            }

            if (display.Rotation == SurfaceOrientation.Rotation270)
            {
                camera.SetDisplayOrientation(180);
            }

            camera.StartPreview();
        }


        void SwitchCameraButtonTapped(object sender, EventArgs e)
        {
            if (cameraType == CameraFacing.Front)
            {
                cameraType = CameraFacing.Back;

                camera.StopPreview();
                camera.Release();
                camera = global::Android.Hardware.Camera.Open((int)cameraType);
                camera.SetPreviewTexture(surfaceTexture);
                PrepareAndStartCamera();
            }
            else
            {
                cameraType = CameraFacing.Front;

                camera.StopPreview();
                camera.Release();
                camera = global::Android.Hardware.Camera.Open((int)cameraType);
                camera.SetPreviewTexture(surfaceTexture);
                PrepareAndStartCamera();
            }
        }

        async void TakePhotoButtonTapped(object sender, EventArgs e)
        {
            camera.StopPreview();

            var image = textureView.Bitmap;
            image = Android.Graphics.Bitmap.CreateBitmap(image, (int)OCR_Rectangle.GetX(), (int)OCR_Rectangle.GetY(), 600, 200);
            image = ToGrayScale(image);

            try
            {
                var absolutePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).AbsolutePath;
                var folderPath = absolutePath + "/Camera";
                filePath = System.IO.Path.Combine(folderPath, string.Format("photo_{0}.jpg", Guid.NewGuid()));

                String recognizedText = "???";
                TessBaseAPI baseApi = new TessBaseAPI();
                baseApi.Init(absolutePath, "eng");              
                baseApi.SetImage(image);
                recognizedText = baseApi.UTF8Text;
                                  
                OCR_textView.Text = "OCR :" + recognizedText;

                var fileStream = new FileStream(filePath, FileMode.Create);
                await image.CompressAsync(Android.Graphics.Bitmap.CompressFormat.Jpeg, 50, fileStream);

                fileStream.Close();
                image.Recycle();

                var intent = new Android.Content.Intent(Android.Content.Intent.ActionMediaScannerScanFile);
                var file = new Java.IO.File(filePath);
                var uri = Android.Net.Uri.FromFile(file);
                intent.SetData(uri);
                MainActivity.Instance.SendBroadcast(intent);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"				", ex.Message);
            }

            camera.StartPreview();
        }

        public Android.Graphics.Bitmap ToGrayScale(Android.Graphics.Bitmap image)
        {

            int width, height;
            height = image.Height;
            width = image.Width;

            Android.Graphics.Bitmap bmpGrayscale = Android.Graphics.Bitmap.CreateBitmap(width, height, Android.Graphics.Bitmap.Config.Argb8888);
            Canvas c = new Canvas(bmpGrayscale);
            Paint paint = new Paint();
            ColorMatrix cm = new ColorMatrix();
            cm.SetSaturation(0);
            ColorMatrixColorFilter f = new ColorMatrixColorFilter(cm);
            paint.SetColorFilter(f);
            c.DrawBitmap(image, 0, 0, paint);
            return bmpGrayscale;
        }

    }
}