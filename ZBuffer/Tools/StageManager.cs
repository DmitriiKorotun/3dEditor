using EmuEngine.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuEngine.Tools
{
    public class StageManager
    {
        public Camera CurrentCamera { get { return currentCamera; } }

        private Camera currentCamera;
        private CameraFrustrum Frustrum { get; set; }
        
        public StageManager()
        {
            SetFrustrum(-160, 160, -90, 90, -50, 50);

            currentCamera = CreateOrthoCamera(
                Frustrum.L, Frustrum.R, Frustrum.B, Frustrum.T, Frustrum.N, Frustrum.F
                );          
        }

        public void SwitchCameraType()
        {
            if (CurrentCamera is OrthographicCamera)
                currentCamera = CreatePerspectiveCamera(
                    Frustrum.L, Frustrum.R, Frustrum.B, Frustrum.T, Frustrum.N, Frustrum.F
                    );
            else
                currentCamera = CreateOrthoCamera(
                    Frustrum.L, Frustrum.R, Frustrum.B, Frustrum.T, Frustrum.N, Frustrum.F
                    );

            currentCamera.ViewMatrix[2, 3] = Frustrum.N - 10;
        }

        public void CreateCamera(float l, float r, float b, float t, float n, float f)
        {
            if (CurrentCamera is OrthographicCamera)
                currentCamera = CreateOrthoCamera(l, r, b, t, n, f);
            else
                currentCamera = CreatePerspectiveCamera(l, r, b, t, n, f);

            

            SetFrustrum(l, r, b, t, n, f);
        }
        
        private OrthographicCamera CreateOrthoCamera(float l, float r, float b, float t, float n, float f)
        {
            return new OrthographicCamera(l, r, b, t, n, f);
        }

        private PerspectiveCamera CreatePerspectiveCamera(float l, float r, float b, float t, float n, float f)
        {
            return new PerspectiveCamera(l, r, b, t, n, f);
        }

        private void SetFrustrum(float l, float r, float b, float t, float n, float f)
        {
            Frustrum = new CameraFrustrum()
            {
                L = l,
                R = r,
                B = b,
                T = t,
                N = n,
                F = f
            };
        }


        private struct CameraFrustrum
        {
            public float L, R, B, T, N, F;
        }
    }
}
