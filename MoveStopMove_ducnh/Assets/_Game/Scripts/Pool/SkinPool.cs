// ///
// /// Simple pooling for Unity.
// ///   Author: Martin "quill18" Glaude (quill18@quill18.com)
// ///   Latest Version: https://gist.github.com/quill18/5a7cfffae68892621267
// ///   License: CC0 (http://creativecommons.org/publicdomain/zero/1.0/)
// ///   UPDATES:
// /// 	2015-04-16:
// /// Support Minh tito CTO ABI games studio
// /// Advantage Linh soi game developer
// ///   UPDATES:
// ///     2017-09-10
// ///     - simple pool with gameobject
// ///     - release game object
// ///     2019-10-09
// ///     - Pool Clamp to keep the quantity within a certain range
// ///     - Pool collect all to despawn all object comeback the pool
// ///     - Spawn with generic T
// ///     - Optimize pool
// ///     2022-10-09
// ///     - Pool with pool type from resources
// ///     - pool with pool container
// ///     2022-11-27
// ///     - Remove clamp pool
// ///     - Spawn in parent transform same instantiate(gameobject, transform)
// ///     - Get list object is actived

// using UnityEngine;
// using System.Collections.Generic;
// using System;
// using GloabalEnum;
// using System.Runtime.InteropServices;

// public static class SkinPool
// {
//     class Pool
//     {
//         //parent contain all pool member
//         Transform m_sRoot = null;
//         //object is can collect back to pool
//         bool m_collect;
//         //list object in pool
//         Queue<SkinUnit> m_inactive;
//         //collect obj active ingame
//         HashSet<SkinUnit> m_active;
//         // The prefab that we are pooling
//         SkinUnit m_prefab;

//         public bool IsCollect { get => m_collect; }
//         public HashSet<SkinUnit> Active => m_active;
//         public int Count => m_inactive.Count + m_active.Count;
//         public Transform Root => m_sRoot;

//         // Constructor
//         public Pool(SkinUnit prefab, int initialQty, Transform parent, bool collect)
//         {
//             m_inactive = new Queue<SkinUnit>(initialQty);
//             m_sRoot = parent;
//             this.m_prefab = prefab;
//             m_collect = collect;
//             if (m_collect) m_active = new HashSet<SkinUnit>();
//         }

//         // Spawn an object from our pool with position and rotation
//         public SkinUnit Spawn(Vector3 pos, Quaternion rot)
//         {
//             SkinUnit obj = Spawn();

//             obj.TF.SetPositionAndRotation(pos, rot);

//             return obj;
//         }

//         //spawn SkinUnit
//         public SkinUnit Spawn()
//         {
//             SkinUnit obj;
//             if (m_inactive.Count == 0)
//             {
//                 obj = (SkinUnit)GameObject.Instantiate(m_prefab, m_sRoot);
//             }
//             else
//             {
//                 // Grab the last object in the inactive array
//                 obj = m_inactive.Dequeue();

//                 if (obj == null)
//                 {
//                     return Spawn();
//                 }
//             }

//             if (m_collect) m_active.Add(obj);

//             obj.gameObject.SetActive(true);

//             return obj;
//         }

//         // Return an object to the inactive pool.
//         public void Despawn(SkinUnit obj)
//         {
//             if (obj != null /*&& !inactive.Contains(obj)*/)
//             {
//                 obj.gameObject.SetActive(false);
//                 m_inactive.Enqueue(obj);

//                 if (memberInParent.Contains(obj.GetInstanceID()))
//                 {
//                     obj.TF.SetParent(GetPool(obj).Root);
//                     memberInParent.Remove(obj.GetInstanceID());
//                 }
//             }

//             if (m_collect) m_active.Remove(obj);
//         }

//         //destroy all unit in pool
//         public void Release()
//         {
//             while (m_inactive.Count > 0)
//             {
//                 SkinUnit go = m_inactive.Dequeue();
//                 GameObject.DestroyImmediate(go);
//             }
//             m_inactive.Clear();
//         }

//         //collect all unit comeback to pool
//         public void Collect()
//         {
//             //while (m_active.Count > 0)
//             //{
//             //    Despawn(m_active[0]);
//             //}


//             HashSet<SkinUnit> units = new HashSet<SkinUnit>(m_active);
//             foreach (var item in units)
//             {
//                 Despawn(item);
//             }
//         }
//     }

//     public const int DEFAULT_POOL_SIZE = 3;

//     //dict for faster search from pool type to prefab
//     static Dictionary<PoolType, SkinUnit> poolTypes = new Dictionary<PoolType, SkinUnit>();

//     //save member that is child transform other object
//     static HashSet<int> memberInParent = new HashSet<int>();

//     private static Transform root;

//     public static Transform Root
//     {
//         get
//         {
//             if (root == null)
//             {
//                 PoolControler controler = GameObject.FindObjectOfType<PoolControler>();
//                 root = controler != null ? controler.transform : new GameObject("Pool").transform;
//             }

//             return root;
//         }
//     }

//     // All of our pools
//     static Dictionary<PoolType, Pool> poolInstance = new Dictionary<PoolType, Pool>();

//     // preload object and pool
//     static public void Preload(SkinUnit prefab, int qty = 1, Transform parent = null, bool collect = false)
//     {
//         if (!poolTypes.ContainsKey(prefab.poolType))
//         {
//             poolTypes.Add(prefab.poolType, prefab);
//         }

//         if (prefab == null)
//         {
//             Debug.LogError(parent.name + " : IS EMPTY!!!");
//             return;
//         }

//         InitPool(prefab, qty, parent, collect);

//         // Make an array to grab the objects we're about to pre-spawn.
//         SkinUnit[] obs = new SkinUnit[qty];
//         for (int i = 0; i < qty; i++)
//         {
//             obs[i] = Spawn(prefab);
//         }

//         // Now despawn them all.
//         for (int i = 0; i < qty; i++)
//         {
//             Despawn(obs[i]);
//         }
//     }

//     // init pool
//     static void InitPool(SkinUnit prefab = null, int qty = DEFAULT_POOL_SIZE, Transform parent = null, bool collect = false)
//     {
//         if (prefab != null && !IsHasPool(prefab))
//         {
//             poolInstance.Add(prefab.poolType, new Pool(prefab, qty, parent, collect));
//         }
//     }
//     static private bool IsHasPool(SkinUnit obj)
//     {
//         return poolInstance.ContainsKey(obj.poolType);
//     }
//     static private Pool GetPool(SkinUnit obj)
//     {
//         return poolInstance[obj.poolType];
//     }
//     public static SkinUnit GetPrefabByType(PoolType poolType)
//     {
//         if (!poolTypes.ContainsKey(poolType) || poolTypes[poolType] == null)
//         {
//             SkinUnit[] resources = Resources.LoadAll<SkinUnit>("Pool");

//             for (int i = 0; i < resources.Length; i++)
//             {
//                 poolTypes[resources[i].poolType] = resources[i];
//             }
//         }

//         return poolTypes[poolType];
//     }

//     #region Get List object ACTIVE
//     // get all member is active in game
//     public static HashSet<SkinUnit> GetAllUnitIsActive(SkinUnit obj)
//     {
//         return IsHasPool(obj) ? GetPool(obj).Active : new HashSet<SkinUnit>();
//     }
//     public static HashSet<SkinUnit> GetAllUnitIsActive(PoolType poolType)
//     {
//         return GetAllUnitIsActive(GetPrefabByType(poolType));
//     }  

//     #endregion

//     #region Spawn
//     // Spawn Unit to use
//     // static public T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : SkinUnit
//     // {
//     //     return Spawn(GetPrefabByType(poolType), pos, rot) as T;
//     // }
//     // static public T Spawn<T>(PoolType poolType) where T : SkinUnit
//     // {
//     //     return Spawn<T>(GetPrefabByType(poolType));
//     // }
//     static public T Spawn<T>(ESkinType eSkinType,SkinUnit obj, Vector3 pos, Quaternion rot) where T : SkinUnit
//     {
//         switch (eSkinType){
//             case SkinType.Hair:
//                 return Spawn(obj, pos, rot) as T;
//             case ESkinType.Shield:
//                 return Spawn(obj, pos, rot) as T;
//         }
        
//     }
//     static public T Spawn<T>(SkinUnit obj) where T : SkinUnit
//     {
//         return Spawn(obj) as T;
//     }

//     // spawn SkinUnit with transform parent
//     static public T Spawn<T>(SkinUnit obj, Transform parent) where T : SkinUnit
//     {
//         return Spawn<T>(obj, obj.TF.localPosition, obj.TF.localRotation, parent);
//     }

//     static public T Spawn<T>(SkinUnit obj, Vector3 localPoint, Quaternion localRot, Transform parent) where T : SkinUnit
//     {
//         T unit = Spawn<T>(obj);
//         unit.TF.SetParent(parent);
//         unit.TF.localPosition = localPoint;
//         unit.TF.localRotation = localRot;
//         unit.TF.localScale = Vector3.one;
//         memberInParent.Add(unit.GetInstanceID());
//         return unit;
//     }

//     static public T Spawn<T> (PoolType poolType, Vector3 localPoint, Quaternion localRot, Transform parent) where T : SkinUnit
//     {
//         return Spawn<T>(GetPrefabByType(poolType), localPoint, localRot, parent);
//     }
//      static public T Spawn<T> (PoolType poolType, Transform parent) where T : SkinUnit
//     {
//         return Spawn<T>(GetPrefabByType(poolType), parent);
//     }

//     static public SkinUnit Spawn(ESkinType skinType,SkinUnit obj, Vector3 pos, Quaternion rot)
//     {
//         if (!IsHasPool(obj))
//         {
//             Transform newRoot = new GameObject(obj.name).transform;
//             newRoot.SetParent(Root);
//             Preload(obj, 1, newRoot, true);
//         }

//         return GetPool(obj).Spawn(pos, rot);
//     }
//     static public SkinUnit Spawn(SkinUnit obj)
//     {
//         if (!IsHasPool(obj))
//         {
//             Transform newRoot = new GameObject(obj.name).transform;
//             newRoot.SetParent(Root);
//             Preload(obj, 1, newRoot, true);
//         }

//         return GetPool(obj).Spawn();
//     }
//     #endregion

//     #region Despawn
//     //take SkinUnit to pool
//     static public void Despawn(SkinUnit obj)
//     {
//         if (obj.gameObject.activeSelf)
//         {
//             if (IsHasPool(obj))
//             {
//                 GetPool(obj).Despawn(obj);
//             }
//             else
//             {
//                 GameObject.Destroy(obj.gameObject);
//             }
//         }
//     }
//     #endregion

//     #region Release
//     //destroy pool
//     static public void Release(SkinUnit obj)
//     {
//         if (IsHasPool(obj))
//         {
//             GetPool(obj).Release();
//         }
//     }
//     static public void Release(PoolType poolType)
//     {
//         Release(GetPrefabByType(poolType));
//     }

//     //DESTROY ALL POOL
//     static public void ReleaseAll()
//     {
//         foreach (var item in poolInstance)
//         {
//             item.Value.Release();
//         }
//     }
//     #endregion

//     #region Collect
//     //collect all pool member comeback to pool
//     static public void Collect(SkinUnit obj)
//     {
//         if (IsHasPool(obj)) GetPool(obj).Collect();
//     }
//     static public void Collect(PoolType poolType)
//     {
//         Collect(GetPrefabByType(poolType));
//     }

//     //COLLECT ALL POOL
//     static public void CollectAll()
//     {
//         foreach (var item in poolInstance)
//         {
//             if (item.Value.IsCollect)
//             {
//                 item.Value.Collect();
//             }
//         }
//     }
//     #endregion
// }
