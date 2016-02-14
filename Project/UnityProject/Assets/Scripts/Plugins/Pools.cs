using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolOption<T>
{
	//Stored Object
	public T m_StoredObject = default(T);
	//this value indicates that the object is using or not
	public bool m_bInUse = false;
}

public class TempConstruct
{
	/// <summary>
	/// Gets the construct.
	/// </summary>
	/// <returns>The construct.</returns>
	/// <param name="args">Arguments.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T GetConstruct<T>(params object[] args)
	{
		try 
		{
			System.Type type = typeof(T);
			List<System.Type> listParamTypes = new List<System.Type>();
			foreach (object item in args)
			{
				listParamTypes.Add(item.GetType());
			}
			System.Reflection.ConstructorInfo ci = type.GetConstructor(listParamTypes.ToArray());
			if (null == ci) 
			{
				return default(T);
			}

			return (T)(ci.Invoke(args));
		} 
		catch 
		{
			throw;
		}
	}
}

public class Pools<T, T1>
{
	//Store all the Objects, different from the identify
	//Get the object with the key value
	private Dictionary<T, List<PoolOption<T1>>> m_PreLoadList = new Dictionary<T, List<PoolOption<T1>>>();
	//
	private int m_iPreloadNumber = 0;

	//In use List
	private List<PoolOption<T1>> m_InUseList = new List<PoolOption<T1>>();
	//Extra use list
	private List<PoolOption<T1>> m_ExtraList = new List<PoolOption<T1>>();

	//This delegate function is used to add a function of hide when 
	//a new object is created
	public delegate void HideFunction(T1 obj);
	public event HideFunction OnActiveObjectFunc; 

	/// <summary>
	/// Initializes a new instance of the <see cref="Pools`2"/> class.
	/// </summary>
	/// <param name="iPreloadNumber">I preload number.</param>
	public Pools(int iPreloadNumber = 1)
	{
		m_iPreloadNumber = iPreloadNumber;
	}
	
	/// <summary>
	/// Adds the stored object with key.
	/// </summary>
	/// <param name="key">Key.</param>
	/// <param name="storedObject">Stored object.</param>
	/// <param name="args">Arguments.</param>
	public void AddStoredObjectWithKey(T key, T1 storedObject, params object[] args)
	{
		if (!m_PreLoadList.ContainsKey(key)) 
		{
			PoolOption<T1> option = new PoolOption<T1>();
			if (option != null)
			{
				option.m_StoredObject = storedObject;
				option.m_bInUse = false;
				
				List<PoolOption<T1>> preList = new List<PoolOption<T1>>();
				if (preList != null)
				{
					m_PreLoadList.Add(key, preList);
				}
				
				if (m_iPreloadNumber <= 0) 
				{
					m_iPreloadNumber = 1;
				}
				
				PoolOption<T1> optionObject = new PoolOption<T1>();
				if (optionObject != null)
				{
					optionObject.m_StoredObject = storedObject;
					optionObject.m_bInUse = false;
					preList.Add(optionObject);
				}
			}
		}
	}
	
	/// <summary>
	/// Gets the object with key.
	/// </summary>
	/// <returns>The object with key.</returns>
	/// <param name="key">Key.</param>
	/// <param name="args">Arguments.</param>
	public T1 GetObjectWithKey(T key, params object[] args)
	{
		//if the preload list contains the object
		if (m_PreLoadList.ContainsKey(key)) 
		{
			List<PoolOption<T1>> poolOptionList = m_PreLoadList[key];
			T1 retObject = default(T1);
			PoolOption<T1> option = null;
			
			for (int i = 0; i < poolOptionList.Count; ++i)
			{
				option = poolOptionList[i];
				if (!option.m_bInUse)
				{
					option.m_bInUse = true;
					retObject = option.m_StoredObject;
					m_InUseList.Add(option);
					break;
				}
			}

			//if all the pre object is in use, create a new one
			if (retObject == null)
			{
				retObject = TempConstruct.GetConstruct<T1>(args);
				//option.m_StoredObject;
				//new T1();//System.Activator.CreateInstance<T1>();
				PoolOption<T1> poolOptionExtra = new PoolOption<T1>();
				if (poolOptionExtra != null)
				{
					poolOptionExtra.m_bInUse = true;
					poolOptionExtra.m_StoredObject = retObject;
					if (poolOptionList.Count < m_iPreloadNumber)
					{
						poolOptionList.Add(poolOptionExtra);
					}
					else 
					{
						m_ExtraList.Add(poolOptionExtra);
					}
				}
			}
			
			return retObject;
		}
		else
		{
			//not contain the value
			return default(T1);
		}
	}
	
	/// <summary>
	/// Restores the object with key.
	/// </summary>
	/// <returns><c>true</c>, if object with key was restored, <c>false</c> otherwise.</returns>
	/// <param name="reObject">Re object.</param>
	public bool RestoreObjectWithKey(T1 reObject)
	{
		bool bRet = false;
		//if the object is in the list of in_use, 
		//remove it and set the value of m_bInUse to false
		for (int i = 0; i < m_InUseList.Count; ++i)
		{
			if (m_InUseList[i].m_StoredObject.Equals(reObject)) 
			{
				m_InUseList[i].m_bInUse = false;
				if (OnActiveObjectFunc != null)
				{
					OnActiveObjectFunc(m_InUseList[i].m_StoredObject);
				}
				m_InUseList.RemoveAt(i);
				break;
			}
		}

		//if the object is in the list of extra_list,
		//just remove it from the list
		for (int i = 0; i < m_ExtraList.Count; ++i)
		{
			if (m_ExtraList[i].m_StoredObject.Equals(reObject)) 
			{
				m_ExtraList.RemoveAt(i);
				bRet = true;
				break;
			}
		}

		return bRet;
	}
	
	/// <summary>
	/// Gets the last in pool.
	/// </summary>
	/// <returns>The last in pool.</returns>
	public List<T1> GetLastInPool()
	{
		List<T1> lstRet = new List<T1>();
		foreach(List<PoolOption<T1>> lstObject in m_PreLoadList.Values)
		{
			for (int i = 0; i < lstObject.Count; ++i)
			{
				lstRet.Add(lstObject[i].m_StoredObject);
			}
		}

		return lstRet;
	}
	
	/// <summary>
	/// Cleans up pool.
	/// </summary>
	/// <param name="cleanFunc">Clean func.</param>
	public void CleanUpPool(CleanFunction cleanFunc)
	{
		foreach(List<PoolOption<T1>> lstObject in m_PreLoadList.Values)
		{
			for (int i = 0; i < lstObject.Count; ++i)
			{
				cleanFunc(lstObject[i].m_StoredObject);
			}
		}

		m_PreLoadList.Clear();

		foreach(PoolOption<T1> obj in m_ExtraList)
		{
			cleanFunc(obj.m_StoredObject);
		}

		foreach(PoolOption<T1> obj in m_InUseList)
		{
			cleanFunc(obj.m_StoredObject);
		}

		m_ExtraList.Clear();
		m_InUseList.Clear();
	}

	//For destory the instance, this function is used to
	//destory instance
	public delegate void CleanFunction(T1 obj);
	
	/// <summary>
	/// Gets the pool list with key.
	/// </summary>
	/// <returns>The pool list with key.</returns>
	/// <param name="key">Key.</param>
	public List<T1> GetPoolListWithKey(T key)
	{
		List<T1> listRet = new List<T1>();
		if (listRet != null)
		{
			if (!m_PreLoadList.ContainsKey(key))
			{
				return listRet;
			}

			List<PoolOption<T1>> listObjects = m_PreLoadList[key];
			if (listObjects != null)
			{
				for (int i = 0; i < listObjects.Count; ++i)
				{
					if (!listObjects[i].m_bInUse)
					{
						listRet.Add(listObjects[i].m_StoredObject);
					}
				}
			}
		}

		return listRet;
	}
}
