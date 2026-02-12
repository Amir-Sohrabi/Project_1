using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyBox
{
	public static class MyExtensions
	{
		public static void SetLeft(this RectTransform rt, float left)
		{
			if (rt == null) return;
			Vector2 oMin = rt.offsetMin;
			oMin.x = left;
			rt.offsetMin = oMin;
		}


		/// <summary>Get the left offset.</summary>
		public static float GetLeft(this RectTransform rt)
		{
			return rt == null ? 0f : rt.offsetMin.x;
		}


		/// <summary>Set the right offset (distance from parent's right edge).</summary>
		public static void SetRight(this RectTransform rt, float right)
		{
			if (rt == null) return;
			Vector2 oMax = rt.offsetMax;
			oMax.x = -right;
			rt.offsetMax = oMax;
		}


		/// <summary>Get the right offset.</summary>
		public static float GetRight(this RectTransform rt)
		{
			return rt == null ? 0f : -rt.offsetMax.x;
		}


		/// <summary>Set the top offset (distance from parent's top edge).</summary>
		public static void SetTop(this RectTransform rt, float top)
		{
			if (rt == null) return;
			Vector2 oMax = rt.offsetMax;
			oMax.y = -top;
			rt.offsetMax = oMax;
		}


		/// <summary>Get the top offset.</summary>
		public static float GetTop(this RectTransform rt)
		{
			return rt == null ? 0f : -rt.offsetMax.y;
		}


		/// <summary>Set the bottom offset (distance from parent's bottom edge).</summary>
		public static void SetBottom(this RectTransform rt, float bottom)
		{
			if (rt == null) return;
			Vector2 oMin = rt.offsetMin;
			oMin.y = bottom;
			rt.offsetMin = oMin;
		}


		/// <summary>Get the bottom offset.</summary>
		public static float GetBottom(this RectTransform rt)
		{
			return rt == null ? 0f : rt.offsetMin.y;
		}


		/// <summary>
		/// Set all four offsets (left, right, top, bottom).
		/// This assumes the rect uses offsets (not fixed size by anchors). It will work with any anchors but results differ depending on anchor setup.
		/// </summary>
		public static void SetPadding(this RectTransform rt, float left, float right, float top, float bottom)
		{
			if (rt == null) return;
			rt.offsetMin = new Vector2(left, bottom);
		}
		
		/// <summary>
		/// Swap two elements in array
		/// </summary>
		public static void Swap<T>(this T[] array, int a, int b) => (array[a], array[b]) = (array[b], array[a]);

		public static bool IsWorldPointInViewport(this Camera camera, Vector3 point)
		{
			var position = camera.WorldToViewportPoint(point);
			return position.x > 0 && position.y > 0;
		}

		/// <summary>
		/// Gets a point with the same screen point as the source point,
		/// but at the specified distance from camera.
		/// </summary>
		public static Vector3 WorldPointOffsetByDepth(this Camera camera,
			Vector3 source,
			float distanceFromCamera,
			Camera.MonoOrStereoscopicEye eye = Camera.MonoOrStereoscopicEye.Mono)
		{
			var screenPoint = camera.WorldToScreenPoint(source, eye);
			return camera.ScreenToWorldPoint(screenPoint.SetZ(distanceFromCamera),
				eye);
		}
		
		
		/// <summary>
		/// Set position to Vector3.zero
		/// </summary>
		public static void ResetPosition(this Transform transform) => transform.position = Vector3.zero;
		

		/// <summary>
		/// Sets the lossy scale of the source Transform.
		/// </summary>
		public static Transform SetLossyScale(this Transform source,
			Vector3 targetLossyScale)
		{
			source.localScale = source.lossyScale.Pow(-1).ScaleBy(targetLossyScale)
				.ScaleBy(source.localScale);
			return source;
		}

		/// <summary>
		/// Sets a layer to the source's attached GameObject and all of its children
		/// in the hierarchy.
		/// </summary>
		public static T SetLayerRecursively<T>(this T source, string layerName)
			where T : Component
		{
			source.gameObject.SetLayerRecursively(LayerMask.NameToLayer(layerName));
			return source;
		}

		/// <summary>
		/// Sets a layer to the source's attached GameObject and all of its children
		/// in the hierarchy.
		/// </summary>
		public static T SetLayerRecursively<T>(this T source, int layer)
			where T : Component
		{
			source.gameObject.SetLayerRecursively(layer);
			return source;
		}

		/// <summary>
		/// Sets a layer to the source GameObject and all of its children in the
		/// hierarchy.
		/// </summary>
		public static GameObject SetLayerRecursively(this GameObject source,
			string layerName)
		{
			source.SetLayerRecursively(LayerMask.NameToLayer(layerName));
			return source;
		}

		/// <summary>
		/// Sets a layer to the source GameObject and all of its children in the
		/// hierarchy.
		/// </summary>
		public static GameObject SetLayerRecursively(this GameObject source, int layer)
		{
			var allTransforms = source.GetComponentsInChildren<Transform>(true);
			foreach (var tf in allTransforms) tf.gameObject.layer = layer;
			return source;
		}


		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
		{
			var toGet = gameObject.GetComponent<T>();
			if (toGet != null) return toGet;
			return gameObject.AddComponent<T>();
		}

		public static T GetOrAddComponent<T>(this Component component) where T : Component 
			=> GetOrAddComponent<T>(component.gameObject);
		

		public static bool HasComponent<T>(this GameObject gameObject) => gameObject.GetComponent<T>() != null;
		public static bool HasComponent<T>(this Component component) => component.GetComponent<T>() != null;

		
		/// <summary>
		/// Recursively get childs that match specific predicate
		/// </summary>
		public static List<Transform> GetChildsWhere(this Transform transform, Predicate<Transform> match)
		{
			List<Transform> list = new List<Transform>();
			RecursiveCheck(transform);
			return list;

			void RecursiveCheck(Transform parent)
			{
				foreach (Transform t in parent)
				{
					RecursiveCheck(t);

					if (match.Invoke(t)) list.Add(t);
				}
			}
		}


		/// <summary>
		/// Get all components of specified Layer in childs
		/// </summary>
		public static List<Transform> GetObjectsOfLayerInChilds(this GameObject gameObject, int layer)
			=> GetChildsWhere(gameObject.transform, t => t.gameObject.layer == layer);


		/// <summary>
		/// Get all components of specified Layer in childs
		/// </summary>
		public static List<Transform> GetObjectsOfLayerInChilds(this GameObject gameObject, string layer)
			=> gameObject.GetObjectsOfLayerInChilds(LayerMask.NameToLayer(layer));

		/// <summary>
		/// Get all components of specified Layer in childs
		/// </summary>
		public static List<Transform> GetObjectsOfLayerInChilds(this Component component, string layer)
			=> component.GetObjectsOfLayerInChilds(LayerMask.NameToLayer(layer));

		/// <summary>
		/// Get all components of specified Layer in childs
		/// </summary>
		public static List<Transform> GetObjectsOfLayerInChilds(this Component component, int layer) 
			=> component.gameObject.GetObjectsOfLayerInChilds(layer);

#if UNITY_PHYSICS_ENABLED

		/// <summary>
		/// Swap Rigidbody IsKinematic and DetectCollisions
		/// </summary>
		/// <param name="body"></param>
		/// <param name="state"></param>
		public static void SetBodyState(this Rigidbody body, bool state)
		{
			body.isKinematic = !state;
			body.detectCollisions = state;
		}

#endif

		/// <summary>
		/// Find all Components of specified interface
		/// </summary>
		public static T[] FindObjectsOfInterface<T>() where T : class
		{
			var monoBehaviours = Object.FindObjectsOfType<Transform>();

			return monoBehaviours.Select(behaviour => behaviour.GetComponent(typeof(T))).OfType<T>().ToArray();
		}

		/// <summary>
		/// Find all Components of specified interface along with Component itself
		/// </summary>
		public static ComponentOfInterface<T>[] FindObjectsOfInterfaceAsComponents<T>() where T : class
		{
			return Object.FindObjectsOfType<Component>()
				.Where(c => c is T)
				.Select(c => new ComponentOfInterface<T>(c, c as T)).ToArray();
		}

		public struct ComponentOfInterface<T>
		{
			public readonly Component Component;
			public readonly T Interface;

			public ComponentOfInterface(Component component, T @interface)
			{
				Component = component;
				Interface = @interface;
			}
		}

		public static void LookAtXZ(this Transform t, Vector3 target)
		{
			target.y = t.position.y;
			t.LookAt(target);
		}

		public static async Task< bool> IsStillPlay(float  delay)
		{
			await Task.Delay(TimeSpan.FromSeconds(delay));

			return Application.isPlaying;
		}

		#region One Per Instance

		/// <summary>
		/// Get components with unique Instance ID
		/// </summary>
		public static T[] OnePerInstance<T>(this T[] components) where T : Component
		{
			if (components == null || components.Length == 0) return null;
			return components.GroupBy(h => h.transform.GetInstanceID()).Select(g => g.First()).ToArray();
		}

#if UNITY_PHYSICS2D_ENABLED

		/// <summary>
		/// Get hits with unique owner Instance ID
		/// </summary>
		public static RaycastHit2D[] OneHitPerInstance(this RaycastHit2D[] hits)
		{
			if (hits == null || hits.Length == 0) return null;
			return hits.GroupBy(h => h.transform.GetInstanceID()).Select(g => g.First()).ToArray();
		}

		/// <summary>
		/// Get colliders with unique owner Instance ID
		/// </summary>
		public static Collider2D[] OneHitPerInstance(this Collider2D[] hits)
		{
			if (hits == null || hits.Length == 0) return null;
			return hits.GroupBy(h => h.transform.GetInstanceID()).Select(g => g.First()).ToArray();
		}

		/// <summary>
		/// Get colliders with unique owner Instance ID
		/// </summary>
		public static List<Collider2D> OneHitPerInstanceList(this Collider2D[] hits)
		{
			if (hits == null || hits.Length == 0) return null;
			return hits.GroupBy(h => h.transform.GetInstanceID()).Select(g => g.First()).ToList();
		}

#endif

		#endregion
		
		public static T CloneJson<T>(this T source)
		{            
			
			// Don't serialize a null object, simply return the default for that object
			if (ReferenceEquals(source, null)) return default;
		
			// initialize inner objects individually
			// for example in default constructor some list property initialized with some values,
			// but in 'source' these items are cleaned -
			// without ObjectCreationHandling.Replace default constructor values will be added to result
			var deserializeSettings = new JsonSerializerSettings {ObjectCreationHandling = ObjectCreationHandling.Replace};
		
			return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
		}
		public static string AssignColor(this string source,string color)
		{  
			return $"<color={color}>{source}</color>";
		}
		public static string AssignColor(this int source,string color)
		{            
			return AssignColor(source.ToString(),color);
		}
		public static string AsGreen(this float source)
		{            
			return AssignColor(source.ToString(),"green");
		}
		public static string AssignUnderLine(this string source)
		{  
			return $"<u>{source}</u>";
		}
		
		public static string GetValueTypeFromName(this string fieldName, Type type)
		{
			FieldInfo field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
			return field != null ? (string)field.GetValue(null) : null;
		}

		public static void DestroyChildren(this Transform t)
		{
			foreach (Transform child in t)
			{
				Object.Destroy(child.gameObject);
			}
		}
		
		public static float DistanceXZ(this Vector3 source, Vector3 target)
		{
			target.y = source.y;
			
			
			return Vector3.Distance(source,target );
		}
	}
}