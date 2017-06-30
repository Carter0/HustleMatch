using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gamelogic.Grids2
{
	[CreateAssetMenu(fileName = "MeshData", menuName = "Grids/MeshData/Uniform Strip")]
	public class MeshStripMeshData : MeshData
	{
		private const float Half = 0.5f;
		public int quadCount;

		public Vector3 bottomLeft = new Vector3(-Half, -Half, 0);
		public Vector3 bottomRight = new Vector3(Half, -Half, 0);
		public Vector3 topLeft = new Vector3(-Half, Half, 0);
		public Vector3 topRight = new Vector3(Half, Half, 0);

		private IEnumerable<Vector3> GetCellVertices()
		{
			for (int i = 0; i <= quadCount; i++)
			{
				float t = i / (float)quadCount;

				yield return Vector3.Lerp(bottomLeft, bottomRight, t);
				yield return Vector3.Lerp(topLeft, topRight, t);
			}
		}

		private IEnumerable<Vector2> GetCellUVs()
		{

			for (int i = 0; i <= quadCount; i++)
			{
				float t = i / (float)quadCount;

				yield return Vector3.Lerp(Vector2.zero, Vector3.right, t);
				yield return Vector3.Lerp(Vector3.up, Vector3.one, t);
			}
		}

		private IEnumerable<Vector3> GetCellNormals()
		{
			for (int i = 0; i <= quadCount; i++)
			{
				yield return Vector3.back;
				yield return Vector3.back;
			}
		}
		private IEnumerable<int> GetCellTriangles()
		{
			for (int i = 0; i < quadCount; i++)
			{
				yield return 0 + 2 * i;
				yield return 1 + 2 * i;
				yield return 3 + 2 * i;

				yield return 3 + 2 * i;
				yield return 0 + 2 * i;
				yield return 2 + 2 * i;
			}

		}

		public override IEnumerable<Vector3> GetVertices(IExplicitShape<int> shape, GridMap<int> map)
		{
			var triangles = GetCellTriangles().ToArray();
			var vertices = GetCellVertices().ToArray();

			return shape.Points.SelectMany(p => vertices.Select(q => map.GridToWorld(q + map.DeRound(p))));
		}

		public override IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint2> shape, GridMap<GridPoint2> map)
		{
			var triangles = GetCellTriangles().ToArray();
			var vertices = GetCellVertices().ToArray();

			return shape.Points.SelectMany(p => vertices.Select(q => map.GridToWorld(q + map.DeRound(p))));
		}

		public override IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint3> shape, GridMap<GridPoint3> map)
		{
			var triangles = GetCellTriangles().ToArray();
			var vertices = GetCellVertices().ToArray();

			return shape.Points.SelectMany(p => vertices.Select(q => map.GridToWorld(q + map.DeRound(p))));
		}

		//TODO doubleSided
		public override IEnumerable<int> GetTriangles(IExplicitShape<int> shape, bool doubleSided, bool flip)
		{
			if (doubleSided) return null;

			if (flip) return GetTrianglesFlipped(shape);

			return GetTrianglesNormal(shape);

		}

		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<int> shape)
		{
			var triangles = GetCellTriangles().ToArray();
			var vertices = GetCellVertices().ToArray();

			return shape.Points.SelectMany((p, i) => triangles.Select(t => t + i * vertices.Length));
		}

		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<int> shape)
		{
			var triangles = GetCellTriangles().ToArray();
			var vertices = GetCellVertices().ToArray();

			var flippedTriangles = new int[triangles.Length];

			for (int i = 0; i < triangles.Length; i += 3)
			{
				flippedTriangles[i] = triangles[i + 1];
				flippedTriangles[i + 1] = triangles[i];
				flippedTriangles[i + 2] = triangles[i + 2];
			}

			return shape.Points.SelectMany((p, i) => flippedTriangles.Select(t => t + i * vertices.Length));
		}

		public override IEnumerable<int> GetTriangles(IExplicitShape<GridPoint2> shape, bool doubleSided, bool flip)
		{
			if (doubleSided) return null;

			if (flip) return GetTrianglesFlipped(shape);

			return GetTrianglesNormal(shape);

		}

		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<GridPoint2> shape)
		{
			var triangles = GetCellTriangles().ToArray();
			var vertices = GetCellVertices().ToArray();
			return shape.Points.SelectMany((p, i) => triangles.Select(t => t + i * vertices.Length));
		}

		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<GridPoint2> shape)
		{
			var triangles = GetCellTriangles().ToArray();
			var vertices = GetCellVertices().ToArray();

			var flippedTriangles = new int[triangles.Length];

			for (int i = 0; i < triangles.Length; i += 3)
			{
				flippedTriangles[i] = triangles[i + 1];
				flippedTriangles[i + 1] = triangles[i];
				flippedTriangles[i + 2] = triangles[i + 2];
			}

			return shape.Points.SelectMany((p, i) => flippedTriangles.Select(t => t + i * vertices.Length));
		}
		public override IEnumerable<int> GetTriangles(IExplicitShape<GridPoint3> shape, bool doubleSided, bool flip)
		{
			if (doubleSided) return null;

			if (flip) return GetTrianglesFlipped(shape);

			return GetTrianglesNormal(shape);

		}

		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<GridPoint3> shape)
		{
			var triangles = GetCellTriangles().ToArray();
			var vertices = GetCellVertices().ToArray();

			return shape.Points.SelectMany((p, i) => triangles.Select(t => t + i * vertices.Length));
		}

		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<GridPoint3> shape)
		{
			var triangles = GetCellTriangles().ToArray();
			var vertices = GetCellVertices().ToArray();

			var flippedTriangles = new int[triangles.Length];

			for (int i = 0; i < triangles.Length; i += 3)
			{
				flippedTriangles[i] = triangles[i + 1];
				flippedTriangles[i + 1] = triangles[i];
				flippedTriangles[i + 2] = triangles[i + 2];
			}

			return shape.Points.SelectMany((p, i) => flippedTriangles.Select(t => t + i * vertices.Length));
		}

		public override IEnumerable<Vector2> GetUVs(IExplicitShape<int> shape)
		{
			var uvs = GetCellUVs();
			return shape.Points.SelectMany(p => uvs);
		}

		public override IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint2> shape)
		{
			var uvs = GetCellUVs();
			return shape.Points.SelectMany(p => uvs);
		}

		public override IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint3> shape)
		{
			var uvs = GetCellUVs();
			return shape.Points.SelectMany(p => uvs);
		}

		public override IEnumerable<Vector3> GetNormals(IExplicitShape<int> shape, GridMap<int> map, bool flip)
		{
			var normals = GetCellNormals().ToArray();
			var vertices = GetCellVertices();

			return shape.Points.SelectMany(p => vertices.Select((q, i) => map.GridToWorld(q + map.DeRound(p) + (flip ? -normals[i] : normals[i])).normalized));
		}

		public override IEnumerable<Vector3> GetNormals(IExplicitShape<GridPoint2> explicitShape, GridMap<GridPoint2> gridMap, bool flip)
		{
			var normals = GetCellNormals().ToArray();
			var vertices = GetCellVertices().ToArray();

			return explicitShape.Points.SelectMany(p => vertices.Select((q, i) => gridMap.GridToWorld(q + gridMap.DeRound(p) + (flip ? -normals[i] : normals[i])).normalized));
		}

		public override IEnumerable<Vector3> GetNormals(IExplicitShape<GridPoint3> explicitShape, GridMap<GridPoint3> gridMap, bool flip)
		{
			var normals = GetCellNormals().ToArray();
			var vertices = GetCellVertices().ToArray();

			return explicitShape.Points.SelectMany(p => vertices.Select((q, i) => gridMap.GridToWorld(q + gridMap.DeRound(p) + (flip ? -normals[i] : normals[i])).normalized));
		}
	}
}