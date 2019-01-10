using EmuEngine.EmuMath;
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
            SetFrustrum(90, 45, 1.0f, 1000.0f);

            currentCamera = CreateOrthoCamera(
                Frustrum.FOV, Frustrum.VFOV, Frustrum.N, Frustrum.F
                );          
        }

        public void RotateCamera(float sx, float sy, float sz)
        {
            if (sx != 0)
                RotateX(currentCamera, sx);

            if (sy != 0)
                RotateY(currentCamera, sy);

            if (sz != 0)
                RotateZ(currentCamera, sz);
        }

        public void MoveCamera(float sx, float sy, float sz)
        {
            float[,] movementMatrix = {
                { 1, 0, 0, sx },
                { 0, 1, 0, sy },
                { 0, 0, 1, sz },
                { 0, 0, 0, 1 }
            };

            currentCamera.ViewMatrix = MatrixMultiplier.MultiplyMatrix(currentCamera.ViewMatrix, movementMatrix);
        }

        public void SwitchCameraType()
        {
            if (CurrentCamera is OrthographicCamera)
                currentCamera = CreatePerspectiveCamera(
                    Frustrum.FOV, Frustrum.VFOV, Frustrum.N, Frustrum.F
                    );
            else
                currentCamera = CreateOrthoCamera(
                    Frustrum.FOV, Frustrum.VFOV, Frustrum.N, Frustrum.F
                    );

            //currentCamera.ViewMatrix[2, 3] = Frustrum.N - 40;
        }

        public void CreateCamera(float fov, float vfov, float n, float f)
        {
            if (CurrentCamera is OrthographicCamera)
                currentCamera = CreateOrthoCamera(fov, vfov, n, f);
            else
                currentCamera = CreatePerspectiveCamera(fov, vfov, n, f);

            

            SetFrustrum(fov, vfov, n, f);
        }
        
        private OrthographicCamera CreateOrthoCamera(float l, float r, float b, float t, float n, float f)
        {
            return new OrthographicCamera(l, r, b, t, n, f);
        }

        private OrthographicCamera CreateOrthoCamera(float fov, float vfov, float n, float f)
        {
            return new OrthographicCamera(fov, vfov, n, f);
        }

        private PerspectiveCamera CreatePerspectiveCamera(float l, float r, float b, float t, float n, float f)
        {
            return new PerspectiveCamera(l, r, b, t, n, f);
        }

        private PerspectiveCamera CreatePerspectiveCamera(float fov, float vfov, float n, float f)
        {
            return new PerspectiveCamera(fov, vfov, n, f);
        }

        private void SetFrustrum(float fov, float vfov, float n, float f)
        {
            Frustrum = new CameraFrustrum()
            {
                FOV = fov,
                VFOV = vfov,
                N = n,
                F = f
            };
        }


        private struct CameraFrustrum
        {
            public float FOV, VFOV, N, F;
        }


        private void RotateX(Camera camera, double angle)
        {
            double rads = angle * Math.PI / 180.0;

            float[,] rotateX = {
                { 1, 0, 0, 0 },
                { 0, (float)Math.Cos(rads), -(float)Math.Sin(rads), 0 },
                { 0, (float)Math.Sin(rads), (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };

            //shape.ModelMatrix = MatrixMultiplier.MultiplyMatrix(rotateX, shape.ModelMatrix);
            camera.ViewMatrix = MatrixMultiplier.MultiplyMatrix(camera.ViewMatrix, rotateX);
        }

        private void RotateY(Camera camera, double angle)
        {
            double rads = angle * Math.PI / 180.0;

            float[,] rotateY = {
                { (float)Math.Cos(rads), 0, (float)Math.Sin(rads), 0 },
                { 0, 1, 0, 0 },
                { -(float)Math.Sin(rads), 0, (float)Math.Cos(rads), 0 },
                { 0, 0, 0, 1 }
            };

            //shape.ModelMatrix = MatrixMultiplier.MultiplyMatrix(rotateY, shape.ModelMatrix);
            camera.ViewMatrix = MatrixMultiplier.MultiplyMatrix(camera.ViewMatrix, rotateY);
        }

        private void RotateZ(Camera camera, double angle)
        {
            double rads = angle * Math.PI / 180.0;

            float[,] rotateZ = {
                { (float)Math.Cos(rads), -(float)Math.Sin(rads), 0, 0 },
                { (float)Math.Sin(rads), (float)Math.Cos(rads), 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };

            //shape.ModelMatrix = MatrixMultiplier.MultiplyMatrix(rotateZ, shape.ModelMatrix);
            camera.ViewMatrix = MatrixMultiplier.MultiplyMatrix(camera.ViewMatrix, rotateZ);
        }
    }
}
