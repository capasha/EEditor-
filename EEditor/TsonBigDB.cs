using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Tson;

namespace TsonBigDB
{
    internal class ObjectProperty
    {
        public string Name { get; set; }
        public ValueObject Value { get; set; }
    }

    internal class ArrayProperty
    {
        public int Index { get; set; }
        public ValueObject Value { get; set; }
    }

    public enum ValueType
    {
        String,
        Int,
        UInt,
        Long,
        Bool,
        Float,
        Double,
        ByteArray,
        DateTime,
        Array,
        Object
    }

    internal class ValueObject
    {
        public ValueType ValueType { get; set; }
        public string String { get; set; }
        public int Int { get; set; }
        public uint UInt { get; set; }
        public long Long { get; set; }
        public bool Bool { get; set; }
        public float Float { get; set; }
        public double Double { get; set; }
        public byte[] ByteArray { get; set; }
        public long DateTime { get; set; }

        public List<ArrayProperty> ArrayProperties { get; set; }
        public List<ObjectProperty> ObjectProperties { get; set; }
    }

    /// <summary>
    /// A database object is an object stored in BigDB which has a unique key, and a set of properties.
    /// <para>
    ///     You can set and remove properties, and persist the changes to the database with the Save() method on the root object.
    /// </para>
    /// </summary>
    public partial class DatabaseObject : IDictionary<string, object>
    {
        /// <summary>
        /// This method allows you to load a Database Object (properties only) from a TSON string.
        /// </summary>
        /// <param name="input"> The TSON string. </param>
        /// <returns> A database object containing the properties of the deserialized TSON. </returns>
        public static DatabaseObject LoadFromString(string input) => DatabaseEx.FromDictionary(TsonConvert.DeserializeObject(input)) as DatabaseObject;

        public DatabaseObject()
        {
            this.Properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// The name of the table the object belongs to.
        /// </summary>
        public string Table { get; internal set; }

        /// <summary>
        /// The key of the object.
        /// </summary>
        public string Key { get; internal set; }

        /// <summary>
        /// The version of the object, incremented every save.
        /// </summary>
        public string Version { get; internal set; }

        /// <summary>
        /// The properties of the object.
        /// </summary>
        public Dictionary<string, object> Properties { get; set; }

        /// <summary>
        /// A boolean representing whether the Database Object has been persisted to BigDB.
        /// </summary>
        internal bool ExistsInDatabase { get; set; }

        public ICollection<object> Values => this.Properties.Values;
        public ICollection<string> Keys => this.Properties.Keys;

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => this.Properties.GetEnumerator();
        public object this[string property] => this.Properties.ContainsKey(property) ? this.Properties[property] : null;
        public object this[string property, System.Type type] => this.Get(property, type);
        private object Get(string property, System.Type type)
        {
            if (!this.Properties.ContainsKey(property) || this.Properties[property] == null)
                throw new PlayerIOClient.PlayerIOError(PlayerIOClient.ErrorCode.GeneralError, (GetType() == typeof(DatabaseArray) ? "The array does not have an entry at: " : "Property does not exist: ") + property);

            if (this.Properties[property].GetType() != type)
                throw new PlayerIOClient.PlayerIOError(PlayerIOClient.ErrorCode.GeneralError, $"No property found with the type '{ type.Name }'.");

            return this.Properties[property];
        }

        public DatabaseObject Set(string property, string value) => this.SetProperty(property, (object)value);
        public DatabaseObject Set(string property, int value) => this.SetProperty(property, (object)value);
        public DatabaseObject Set(string property, uint value) => this.SetProperty(property, (object)value);
        public DatabaseObject Set(string property, long value) => this.SetProperty(property, (object)value);
        public DatabaseObject Set(string property, ulong value) => this.SetProperty(property, (object)value);
        public DatabaseObject Set(string property, float value) => this.SetProperty(property, (object)value);
        public DatabaseObject Set(string property, double value) => this.SetProperty(property, (object)value);
        public DatabaseObject Set(string property, bool value) => this.SetProperty(property, (object)value);
        public DatabaseObject Set(string property, byte[] value) => this.SetProperty(property, (object)value);
        public DatabaseObject Set(string property, DateTime value) => this.SetProperty(property, (object)value);
        public DatabaseObject Set(string property, DatabaseObject value) => this.SetProperty(property, (object)value);
        public DatabaseObject Set(string property, DatabaseArray value) => this.SetProperty(property, (object)value);

        public virtual DatabaseObject SetProperty(string property, object value)
        {
            if (property.Contains("."))
                throw new InvalidOperationException("You must not include periods within the property name.");

            var allowedTypes = new List<Type>()
            {
                typeof(string), typeof(int),    typeof(uint),
                typeof(long),   typeof(ulong),  typeof(float),
                typeof(double), typeof(bool),   typeof(byte[]),
                typeof(DateTime), typeof(DatabaseObject), typeof(DatabaseArray)
            };

            if (value != null && !allowedTypes.Contains(value.GetType()))
                throw new PlayerIOClient.PlayerIOError(PlayerIOClient.ErrorCode.GeneralError, $"The type '{ value.GetType().Name }' is not allowed.");

            if (!this.Properties.ContainsKey(property))
                this.Properties.Add(property, value);
            else this.Properties[property] = value;

            return this;
        }

        public bool GetBool(string prop) => (bool)this[prop, typeof(bool)];
        public bool GetBool(string prop, bool defaultValue) => this[prop] is bool value ? value : defaultValue;

        public byte[] GetBytes(string prop) => (byte[])this[prop, typeof(byte[])];
        public byte[] GetBytes(string prop, byte[] defaultValue) => this[prop] is byte[] value ? value : defaultValue;

        public double GetDouble(string prop) => (double)this[prop, typeof(double)];
        public double GetDouble(string prop, double defaultValue) => this[prop] is double value ? value : defaultValue;

        public float GetFloat(string prop) => (float)this[prop, typeof(float)];
        public float GetFloat(string prop, float defaultValue) => this[prop] is float value ? value : defaultValue;

        public int GetInt(string prop) => (int)this[prop, typeof(int)];
        public int GetInt(string prop, int defaultValue) => this[prop] is int value ? value : defaultValue;

        public uint GetUInt(string prop) => (uint)this[prop, typeof(uint)];
        public uint GetUInt(string prop, uint defaultValue) => this[prop] is uint value ? value : defaultValue;

        public long GetLong(string prop) => (long)this[prop, typeof(long)];
        public long GetLong(string prop, long defaultValue) => this[prop] is long value ? value : defaultValue;

        public string GetString(string prop) => (string)this[prop, typeof(string)];
        public string GetString(string prop, string defaultValue) => this[prop] is string value ? value : defaultValue;

        public DateTime GetDateTime(string prop) => (DateTime)this[prop, typeof(DateTime)];
        public DateTime GetDateTime(string prop, DateTime defaultValue) => this[prop] is DateTime value ? value : defaultValue;

        public DatabaseObject GetObject(string prop) => (DatabaseObject)this[prop, typeof(DatabaseObject)];
        public DatabaseObject GetObject(string prop, DatabaseObject defaultValue) => this[prop] is DatabaseObject value ? value : defaultValue;

        public DatabaseArray GetArray(string prop) => (DatabaseArray)this[prop, typeof(DatabaseArray)];
        public DatabaseArray GetArray(string prop, DatabaseArray defaultValue) => this[prop] is DatabaseArray value ? value : defaultValue;

        /// <summary>
        /// Check whether this object contains the specified property.
        /// </summary>
        /// <param name="property"> The name of the property. </param>
        /// <returns> If the object contains the property, returns true. </returns>
        public bool ContainsKey(string property) => this.Properties.ContainsKey(property);

        /// <summary>
        /// Removes a property from this object.
        /// </summary>
        /// <param name="property"> The property to remove. </param>
        /// <returns> If the property has been successfully removed, returns true.  </returns>
        public bool Remove(string property) => this.Properties.Remove(property);

        /// <summary>
        /// Removes all properties on this object.
        /// </summary>
        public void Clear() => this.Properties.Clear();

        /// <summary>
        /// The amount of properties within this object.
        /// </summary>
        public int Count => this.Properties.Count;

        public bool IsReadOnly => ((ICollection<KeyValuePair<string, object>>)this.Properties).IsReadOnly;

        object IDictionary<string, object>.this[string key] { get => ((IDictionary<string, object>)this.Properties)[key]; set => ((IDictionary<string, object>)this.Properties)[key] = value; }

        /// <summary>
        /// Return a TSON string of the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => TsonConvert.SerializeObject(this.Properties, Formatting.Indented);

        public void Add(string key, object value)
        {
            ((IDictionary<string, object>)this.Properties).Add(key, value);
        }

        public bool TryGetValue(string key, out object value)
        {
            return ((IDictionary<string, object>)this.Properties).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            ((ICollection<KeyValuePair<string, object>>)this.Properties).Add(item);
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)this.Properties).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, object>>)this.Properties).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)this.Properties).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.Properties).GetEnumerator();
        }
    }

    public class DatabaseArray : DatabaseObject, IEnumerable<object>
    {
        public new object[] Values => Properties.Values.ToArray();
        public object this[uint index] => index <= Values.Length - 1 ? Values[index] ?? null : throw new System.IndexOutOfRangeException(nameof(index));

        public DatabaseArray Set(uint index, object value) => SetProperty(index.ToString(), value) as DatabaseArray;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public DatabaseArray Add(object value) => Set((uint)this.Properties.Count, value);

        /// <summary> Add the given string value to the array. </summary>
        public DatabaseArray Add(string value) => Set((uint)this.Properties.Count, value);

        /// <summary> Add the given int value to the array. </summary>
        public DatabaseArray Add(int value) => Set((uint)this.Properties.Count, value);

        /// <summary> Add the given uint value to the array. </summary>
        public DatabaseArray Add(uint value) => Set((uint)this.Properties.Count, value);

        /// <summary> Add the given long value to the array. </summary>
        public DatabaseArray Add(long value) => Set((uint)this.Properties.Count, value);

        /// <summary> Add the given ulong value to the array. </summary>
        public DatabaseArray Add(ulong value) => Set((uint)this.Properties.Count, value);

        /// <summary> Add the given float value to the array. </summary>
        public DatabaseArray Add(float value) => Set((uint)this.Properties.Count, value);

        /// <summary> Add the given double value to the array. </summary>
        public DatabaseArray Add(double value) => Set((uint)this.Properties.Count, value);

        /// <summary> Add the given boolean value to the array. </summary>
        public DatabaseArray Add(bool value) => Set((uint)this.Properties.Count, value);

        /// <summary> Add the given byte array value to the array. </summary>
        public DatabaseArray Add(byte[] value) => Set((uint)this.Properties.Count, value);

        /// <summary> Add the given date time value to the array. </summary>
        public DatabaseArray Add(System.DateTime value) => Set((uint)this.Properties.Count, value);

        /// <summary> Add the given object to the array. </summary>
        public DatabaseArray Add(DatabaseObject value) => Set((uint)this.Properties.Count, value);

        /// <summary> Add the given array to the array. </summary>
        public DatabaseArray Add(DatabaseArray value) => Set((uint)this.Properties.Count, value);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override DatabaseObject SetProperty(string index, object value)
        {
            if (!int.TryParse(index, out var i))
                throw new System.Exception("You must specify the index as an integer.");

            for (var j = this.Properties.Count; j < i; j++)
                base.SetProperty(j.ToString(), null);

            return base.SetProperty(index, value);
        }

        public bool GetBool(uint index) => GetBool(index.ToString());
        public bool GetBool(uint index, bool defaultValue) => GetBool(index.ToString(), defaultValue);

        public byte[] GetBytes(uint index) => GetBytes(index.ToString());
        public byte[] GetBytes(uint index, byte[] defaultValue) => GetBytes(index.ToString(), defaultValue);

        public double GetDouble(uint index) => GetDouble(index.ToString());
        public double GetDouble(uint index, double defaultValue) => GetDouble(index.ToString(), defaultValue);

        public float GetFloat(uint index) => GetFloat(index.ToString());
        public float GetFloat(uint index, float defaultValue) => GetFloat(index.ToString(), defaultValue);

        public int GetInt(uint index) => GetInt(index.ToString());
        public int GetInt(uint index, int defaultValue) => GetInt(index.ToString(), defaultValue);

        public uint GetUInt(uint index) => GetUInt(index.ToString());
        public uint GetUInt(uint index, uint defaultValue) => GetUInt(index.ToString(), defaultValue);

        public long GetLong(uint index) => GetLong(index.ToString());
        public long GetLong(uint index, long defaultValue) => GetLong(index.ToString(), defaultValue);

        public string GetString(uint index) => GetString(index.ToString());
        public string GetString(uint index, string defaultValue) => GetString(index.ToString(), defaultValue);

        public DatabaseObject GetObject(uint index) => GetObject(index.ToString());
        public DatabaseObject GetObject(uint index, DatabaseObject defaultValue) => GetObject(index.ToString(), defaultValue);

        public DatabaseArray GetArray(uint index) => GetArray(index.ToString());
        public DatabaseArray GetArray(uint index, DatabaseArray defaultValue) => GetArray(index.ToString(), defaultValue);

        public new IEnumerator<object> GetEnumerator() => ((IEnumerable<object>)Values).GetEnumerator();
    }

    internal static class DatabaseEx
    {
        public static object ToDictionary(object input)
        {
            var dictionary = new Dictionary<string, object>();

            switch (input)
            {
                case List<ObjectProperty> databaseObject:
                    foreach (var property in databaseObject)
                        dictionary.Add(property.Name, ToDictionary(property.Value));
                    break;

                case ValueObject valueObject:
                    var value = Value(valueObject);

                    if (valueObject.ValueType == ValueType.Array && value == null)
                        return new DatabaseArray();

                    if (valueObject.ValueType == ValueType.Object && value == null)
                        return new DatabaseObject();

                    if (value is List<ObjectProperty> object_properties)
                    {
                        foreach (var property in object_properties)
                            dictionary.Add(property.Name, ToDictionary(property.Value));
                    }
                    else if (value is List<ArrayProperty> array_properties)
                    {
                        var array = new object[array_properties.Count];
                        for (var i = 0; i < array_properties.Count; i++)
                            array[i] = ToDictionary(array_properties[i].Value);

                        return array;
                    }
                    else
                    {
                        return ToDictionary(value);
                    }

                    break;

                case null: return null;
                default: return input;
            }

            return dictionary;
        }

        public static List<ObjectProperty> FromDatabaseObject(DatabaseObject input)
        {
            var model = new List<ObjectProperty>();

            foreach (var kvp in input.Properties.Where(kvp => kvp.Value != null))
            {
                if (kvp.Value.GetType() == typeof(DatabaseObject))
                {
                    model.Add(new ObjectProperty() { Name = kvp.Key, Value = new ValueObject() { ValueType = ValueType.Object, ObjectProperties = FromDatabaseObject(kvp.Value as DatabaseObject) } });
                }
                else if (kvp.Value.GetType() == typeof(DatabaseArray))
                {
                    model.Add(new ObjectProperty() { Name = kvp.Key, Value = new ValueObject() { ValueType = ValueType.Array, ArrayProperties = FromDatabaseArray(kvp.Value as DatabaseArray) } });
                }
                else
                {
                    model.Add(new ObjectProperty() { Name = kvp.Key, Value = Create(kvp.Value) });
                }
            }

            return model;
        }

        public static List<ArrayProperty> FromDatabaseArray(DatabaseArray input)
        {
            var model = new List<ArrayProperty>();

            for (var i = 0; i < input.Values.Length; i++)
            {
                var value = input.Values[i];

                if (value is DatabaseArray array)
                {
                    model.AddRange(FromDatabaseArray(array));
                }
                else if (value is DatabaseObject obj)
                {
                    model.Add(new ArrayProperty() { Index = i, Value = new ValueObject() { ValueType = ValueType.Object, ObjectProperties = FromDatabaseObject(obj) } });
                }
                else
                {
                    model.Add(new ArrayProperty() { Index = i, Value = Create(value) });
                }
            }

            return model;
        }

        public static object FromDictionary(object input)
        {
            var model = new DatabaseObject();

            if (input is Dictionary<string, object>)
            {
                foreach (var kvp in input as Dictionary<string, object>)
                {
                    if (kvp.Value is Dictionary<string, object>)
                    {
                        model.SetProperty(kvp.Key, FromDictionary(kvp.Value as Dictionary<string, object>));
                    }
                    else if (kvp.Value is object[])
                    {
                        var array = new DatabaseArray();

                        foreach (var value in kvp.Value as object[])
                        {
                            array.Add(FromDictionary(value));
                        }

                        model.SetProperty(kvp.Key, array);
                    }
                    else if (kvp.Value is List<object>)
                    {
                        var array = new DatabaseArray();

                        foreach (var value in kvp.Value as List<object>)
                        {
                            array.Add(FromDictionary(value));
                        }

                        model.SetProperty(kvp.Key, array);
                    }
                    else
                    {
                        model.SetProperty(kvp.Key, kvp.Value);
                    }
                }

                return model;
            }
            else
            {
                return input;
            }
        }

        internal static List<ValueObject> MakeRange(object[] indexPath, object tail)
        {
            var result = new object[((indexPath == null) ? 0 : indexPath.Length) + ((tail == null) ? 0 : 1)];

            if (indexPath != null)
                Array.Copy(indexPath, result, indexPath.Length);

            if (tail != null)
                result[result.Length - 1] = tail;

            return result.Select(value => DatabaseEx.Create(value)).ToList();
        }

        internal static ValueObject Create(object value)
        {
            switch (value)
            {
                case string temp: return new ValueObject { ValueType = ValueType.String, String = temp };
                case int temp: return new ValueObject { ValueType = ValueType.Int, Int = temp };
                case uint temp: return new ValueObject { ValueType = ValueType.UInt, UInt = temp };
                case long temp: return new ValueObject { ValueType = ValueType.Long, Long = temp };
                case float temp: return new ValueObject { ValueType = ValueType.Float, Float = temp };
                case double temp: return new ValueObject { ValueType = ValueType.Double, Double = temp };
                case bool temp: return new ValueObject { ValueType = ValueType.Bool, Bool = temp };
                case byte[] temp: return new ValueObject { ValueType = ValueType.ByteArray, ByteArray = temp };
                case DateTime temp: return new ValueObject { ValueType = ValueType.DateTime, DateTime = temp.ToUnixTime() };

                default: throw new ArgumentException($"The type { value.GetType().FullName } is not supported.", nameof(value));
            }
        }

        internal static object Value(this ArrayProperty property) => Value(property.Value);
        internal static object Value(this ObjectProperty property) => Value(property.Value);
        internal static object Value(ValueObject value)
        {
            switch (value.ValueType)
            {
                case ValueType.String: return value.String;
                case ValueType.Int: return value.Int;
                case ValueType.UInt: return value.UInt;
                case ValueType.Long: return value.Long;
                case ValueType.Bool: return value.Bool;
                case ValueType.Float: return value.Float;
                case ValueType.Double: return value.Double;
                case ValueType.ByteArray: return value.ByteArray;
                case ValueType.DateTime: return new DateTime(1970, 1, 1).AddMilliseconds(value.DateTime);
                case ValueType.Array: return value.ArrayProperties;
                case ValueType.Object: return value.ObjectProperties;

                default: return null;
            }
        }
    }

    internal static class DateTimeEx
    {
        internal static DateTime FromUnixTime(this long input) => new DateTime(1970, 1, 1).AddMilliseconds((long)input);
        internal static long ToUnixTime(this DateTime input) => (long)((input - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
    }
}
