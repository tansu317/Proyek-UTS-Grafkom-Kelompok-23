using LearnOpenTK.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using System;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Text;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace PROJEK_GRAFKOM
{
    internal class Asset3D
    {
        List<Vector3> _vertices = new List<Vector3>();
        List<uint> _indices = new List<uint>();


        int _vertexBufferObject;
        int _vertexArrayObject;
        int _elementBufferObject;
        Shader _shader;
        Matrix4 _model;
        Matrix4 _view;
        Matrix4 _projection;
        public List<Asset3D> Child;
        Vector3 _color;

        public Vector3 _centerPosition = new Vector3(0, 0, 0);
        public List<Vector3> _euler = new List<Vector3>();

        public Asset3D(List<Vector3> vertices, List<uint> indices, Vector3 color)
        {
            this._color = color;
            this._vertices = vertices;
            this._indices = indices;
            setdefault();
        }

        public Asset3D(Vector3 color)
        {
            _color = color;

            _vertices = new List<Vector3>();
            //_shader.SetVector3;
            setdefault();
        }
        public void setdefault()
        {
            _euler = new List<Vector3>();
            //sumbu X
            _euler.Add(new Vector3(1, 0, 0));
            //sumbu y
            _euler.Add(new Vector3(0, 1, 0));
            //sumbu z
            _euler.Add(new Vector3(0, 0, 1));
            _model = Matrix4.Identity;
            _centerPosition = new Vector3(0, 0, 0);
            Child = new List<Asset3D>();

        }

        public Asset3D()
        {
            _vertices = new List<Vector3>();
            //sumbu X
            _euler.Add(new Vector3(1, 0, 0));
            //sumbu y
            _euler.Add(new Vector3(0, 1, 0));
            //sumbu z
            _euler.Add(new Vector3(0, 0, 1));

        }

        public void load(string shadervert, string shaderfrag, float Size_X, float Size_Y)
        {
            //VBO
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * Vector3.SizeInBytes,
                _vertices.ToArray(), BufferUsageHint.StaticDraw);

            //VAO
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);


            GL.VertexAttribPointer(0,       //tempat penyimpanan shader
                3,       //jumlah vertices
                VertexAttribPointerType.Float,
                false,                      //tidak pakai normalisasi
                3 * sizeof(float),          //ukuran byte dalam 1 vertex
                0                           //baca mulai dari index 0
                );
            GL.EnableVertexAttribArray(0); //refrence dari yang atas

            //GL.GetInteger(GetPName.MaxVertexAttribs, out int maxAttributeCount);
            //Console.WriteLine($"Maximum number of vertex attribute supported:{maxAttributeCount}");


            if (_indices.Count != 0)
            {
                //EBO
                _elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Count * sizeof(uint), _indices.ToArray(), BufferUsageHint.StaticDraw);

            }

            _shader = new Shader(shadervert, shaderfrag);
            _shader.Use();

            //_view = Matrix4.CreateTranslation(0.0f, 0.0f, -5.0f);


            //_projection = Matrix4.CreatePerspectiveFieldOfView(
            //    MathHelper.DegreesToRadians(45f), Size_X / (float)Size_Y,
            //    0.1f, 100.0f);

            //foreach (var item in Child)
            //{
            //    item.load(shadervert, shaderfrag, Size_X, Size_Y);
            //}
        }

        public void createBoxVertices(float x, float y, float z, float xlength, float ylength, float zlength)
        {
            _centerPosition.X = x;
            _centerPosition.Y = y;
            _centerPosition.Z = z;
            Vector3 temp_vector;

            //TITIK 1
            temp_vector.X = x - xlength / 2.0f;
            temp_vector.Y = y + ylength / 2.0f;
            temp_vector.Z = z - zlength / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 2
            temp_vector.X = x + xlength / 2.0f;
            temp_vector.Y = y + ylength / 2.0f;
            temp_vector.Z = z - zlength / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 3
            temp_vector.X = x - xlength / 2.0f;
            temp_vector.Y = y - ylength / 2.0f;
            temp_vector.Z = z - zlength / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 4
            temp_vector.X = x + xlength / 2.0f;
            temp_vector.Y = y - ylength / 2.0f;
            temp_vector.Z = z - zlength / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 5
            temp_vector.X = x - xlength / 2.0f;
            temp_vector.Y = y + ylength / 2.0f;
            temp_vector.Z = z + zlength / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 6
            temp_vector.X = x + xlength / 2.0f;
            temp_vector.Y = y + ylength / 2.0f;
            temp_vector.Z = z + zlength / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 7
            temp_vector.X = x - xlength / 2.0f;
            temp_vector.Y = y - ylength / 2.0f;
            temp_vector.Z = z + zlength / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 8
            temp_vector.X = x + xlength / 2.0f;
            temp_vector.Y = y - ylength / 2.0f;
            temp_vector.Z = z + zlength / 2.0f;
            _vertices.Add(temp_vector);

            _indices = new List<uint>
            {
                //SEGITIGA DEPAN 1
                0,1,2,
                //SEGITIGA DEPAN 2
                1,2,3,
                //SEGITIGA ATAS 1
                0,4,5,
                //SEGITIGA ATAS 2
                0,1,5,
                //SEGITIGA KANAN 1
                1,3,5,
                //SEGITIGA KANAN 2
                3,5,7,
                //SEGITIGA KIRI 1
                0,2,4,
                //SEGITIGA KIRI 2
                2,4,6,
                //SEGITIGA BELAKANG 1
                4,5,6,
                //SEGITIGA BELAKANG 2
                5,6,7,
                //SEGITIGA BAWAH 1
                2,3,6,
                //SEGITIGA BAWAH 2
                3,6,7
            };
        }
        //public void render(int _lines, double time, Matrix4 temp, Matrix4 camera_view, Matrix4 camera_projection)
        

            public void render(int _lines, double time, Matrix4 temp, Matrix4 camera_view, Matrix4 camera_projection)
        {
            _shader.Use();


            GL.BindVertexArray(_vertexArrayObject);

            //_model = Matrix4.Identity * Matrix4.CreateTranslation(0.0f, 0.5f, 0.0f);
            //_model = _model * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians((float)time * 10f));

            _model = temp;
            _shader.SetMatrix4("model", _model);
            _shader.SetMatrix4("view", camera_view);
            _shader.SetMatrix4("projection", camera_projection);
            _shader.SetVector3("color", _color);

            //_shader.SetMatrix4("view", camera_view);
            //_shader.SetMatrix4("projection", camera_projection);

            if (_indices.Count != 0)
            {
                GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
                //Console.WriteLine(_indices.Count);
            }

            else
            {
                if (_lines == 0)
                {
                    GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Count);
                }
                else if (_lines == 1)
                {
                    GL.DrawArrays(PrimitiveType.TriangleFan, 0, _vertices.Count);
                }
                else if (_lines == 2)
                {

                }
                else if (_lines == 3)
                {
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, _vertices.Count);
                }
            }
        }
        public void createEllipsoid(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z)
        {
            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = -pi; u <= pi; u += pi / 1000)
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 1000)
                {
                    temp_vector.X = _x + (float)Math.Cos(v) * (float)Math.Cos(u) * radiusX;
                    temp_vector.Y = _y + (float)Math.Cos(v) * (float)Math.Sin(u) * radiusY;
                    temp_vector.Z = _z + (float)Math.Sin(v) * radiusZ;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createElipticCone(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, float dir)
        {
            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = -pi; u <= pi; u += pi / 4500)
            {
                for (float v = 0; v <= 1; v += pi / 4500)
                {
                    temp_vector.X = _x + (float)Math.Cos(u) * radiusX * v;
                    temp_vector.Y = _y + (float)Math.Sin(u) * radiusY * v;
                    temp_vector.Z = dir * (_z + v * radiusZ);
                    _vertices.Add(temp_vector);
                }
            }
        }
        public void createEllipsoid2(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int sectorCount, int stackCount)
        {
            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            float sectorStep = 2 * (float)Math.PI / sectorCount;
            float stackStep = (float)Math.PI / stackCount;
            float sectorAngle, StackAngle, x, y, z;

            for (int i = 0; i <= stackCount; ++i)
            {
                StackAngle = pi / 2 - i * stackStep;
                x = radiusX * (float)Math.Cos(StackAngle);
                y = radiusY * (float)Math.Cos(StackAngle);
                z = radiusZ * (float)Math.Sin(StackAngle);

                for (int j = 0; j <= sectorCount; ++j)
                {
                    sectorAngle = j * sectorStep;

                    temp_vector.X = x * (float)Math.Cos(sectorAngle);
                    temp_vector.Y = y * (float)Math.Sin(sectorAngle);
                    temp_vector.Z = z;
                    _vertices.Add(temp_vector);
                }
            }

            uint k1, k2;
            for (int i = 0; i < stackCount; ++i)
            {
                k1 = (uint)(i * (sectorCount + 1));
                k2 = (uint)(k1 + sectorCount + 1);
                for (int j = 0; j < sectorCount; ++j, ++k1, ++k2)
                {
                    if (i != 0)
                    {
                        _indices.Add(k1);
                        _indices.Add(k2);
                        _indices.Add(k1 + 1);
                    }
                    if (i != (stackCount - 1))
                    {
                        _indices.Add(k1 + 1);
                        _indices.Add(k2);
                        _indices.Add(k2 + 1);
                    }
                }
            }
        }

        public void createEllipticParaboloid(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z)
        {
            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = -pi; u <= pi; u += pi / 1000)
            {
                for (float v = 0; v <= pi / 2; v += pi / 1000)
                {
                    temp_vector.X = _x + (float)Math.Cos(u) * radiusX * v;
                    temp_vector.Y = _y + (float)Math.Sin(u) * radiusY * v;
                    temp_vector.Z = _z + v * v;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createHyperboloid(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z)
        {
            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = -pi; u <= pi; u += pi / 1000)
            {
                for (float v = 0; v <= pi / 2; v += pi / 1000)
                {
                    temp_vector.X = _x + (1/(float)Math.Cos(v)) * (float)Math.Cos(u) * radiusX;
                    temp_vector.Y = _y + (1 / (float)Math.Cos(v)) * (float)Math.Sin(u) * radiusY;
                    temp_vector.Z = _z + (float)Math.Sin(v) / (float)Math.Cos(v) * radiusZ;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createHyperboloidParaboloid(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z)
        {
            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = -pi; u <= pi; u += pi / 1000)
            {
                for (float v = 0; v <= 3 / 2; v += pi / 1000)
                {
                    temp_vector.X = _x + (float)Math.Tan(u) * radiusX * v;
                    temp_vector.Y = _y + (float)(1 / Math.Cos(u)) * radiusY * v;
                    temp_vector.Z = _z + v * v;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createHyperBolloid2Sheet(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z)
        {
            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = -pi / 2; u <= pi / 2; u += pi / 1000)
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 1000)
                {
                    temp_vector.X = _x + (float)Math.Tan(v) * (float)Math.Cos(u) * radiusX / 1000000;
                    temp_vector.Y = _y + (float)Math.Tan(v) * (float)Math.Sin(u) * radiusY / 1000000;
                    temp_vector.Z = _z + (float)(1 / Math.Cos(v)) * radiusZ / 1000000;
                    _vertices.Add(temp_vector);
                }
            }
            for (float u = pi / 2; u <= 3 * pi / 2; u += pi / 1000)
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 1000)
                {
                    temp_vector.X = _x + (float)Math.Tan(v) * (float)Math.Cos(u) * radiusX / 1000000;
                    temp_vector.Y = _y + (float)Math.Tan(v) * (float)Math.Sin(u) * radiusY / 1000000;
                    temp_vector.Z = _z + (float)(1 / Math.Cos(v)) * radiusZ / 1000000;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void rotate(Vector3 pivot, Vector3 vector, float angle)
        {
            //pivot -> mau rotate di titik mana
            //vector -> mau rotate di sumbu apa? (x,y,z)
            //angle -> rotatenya berapa derajat?
            var real_angle = angle;
            angle = MathHelper.DegreesToRadians(angle);

            //mulai ngerotasi
            for (int i = 0; i < _vertices.Count; i++)
            {
                _vertices[i] = getRotationResult(pivot, vector, angle, _vertices[i]);
            }
            //rotate the euler direction
            for (int i = 0; i < 3; i++)
            {
                _euler[i] = getRotationResult(pivot, vector, angle, _euler[i], true);

                //NORMALIZE
                //LANGKAH - LANGKAH
                //length = akar(x^2+y^2+z^2)
                float length = (float)Math.Pow(Math.Pow(_euler[i].X, 2.0f) + Math.Pow(_euler[i].Y, 2.0f) + Math.Pow(_euler[i].Z, 2.0f), 0.5f);
                Vector3 temporary = new Vector3(0, 0, 0);
                temporary.X = _euler[i].X / length;
                temporary.Y = _euler[i].Y / length;
                temporary.Z = _euler[i].Z / length;
                _euler[i] = temporary;
            }
            _centerPosition = getRotationResult(pivot, vector, angle, _centerPosition);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * Vector3.SizeInBytes,
                _vertices.ToArray(), BufferUsageHint.StaticDraw);
            foreach (var item in Child)
            {
                item.rotate(pivot, vector, real_angle);
            }
        }
        public Vector3 getRotationResult(Vector3 pivot, Vector3 vector, float angle, Vector3 point, bool isEuler = false)
        {
            Vector3 temp, newPosition;

            if (isEuler)
            {
                temp = point;
            }
            else
            {
                temp = point - pivot;
            }

            newPosition.X =
                temp.X * (float)(Math.Cos(angle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(angle))) +
                temp.Y * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) - vector.Z * Math.Sin(angle)) +
                temp.Z * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) + vector.Y * Math.Sin(angle));

            newPosition.Y =
                temp.X * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) + vector.Z * Math.Sin(angle)) +
                temp.Y * (float)(Math.Cos(angle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(angle))) +
                temp.Z * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) - vector.X * Math.Sin(angle));

            newPosition.Z =
                temp.X * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) - vector.Y * Math.Sin(angle)) +
                temp.Y * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) + vector.X * Math.Sin(angle)) +
                temp.Z * (float)(Math.Cos(angle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(angle)));

            if (isEuler)
            {
                temp = newPosition;
            }
            else
            {
                temp = newPosition + pivot;
            }
            return temp;
        }

        public void resetEuler()
        {
            _euler[0] = new Vector3(1, 0, 0);
            _euler[1] = new Vector3(0, 1, 0);
            _euler[2] = new Vector3(0, 0, 1);
        }

        public bool getVerticesLength()
        {
            if (_vertices.Count == 0)
            {
                return false;
            }
            if (_vertices.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
