using SkyStrike.Game;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Editor
{
    public class BulletDataObserver : IEditorData<EnemyBulletMetaData, BulletDataObserver>, IDataList<BulletStateDataObserver>
    {
        public static readonly int UNDEFINED_ID = -1;
        public int id { get; set; }
        public DataObserver<string> name { get; private set; }
        public DataObserver<float> size { get; private set; }
        public DataObserver<float> speed { get; private set; }
        public DataObserver<float> timeCooldown { get; private set; }
        public DataObserver<float> spinSpeed { get; private set; }
        public DataObserver<float> lifetime { get; private set; }
        public DataObserver<float> unitAngle { get; private set; }
        public DataObserver<float> startAngle { get; private set; }
        public DataObserver<Vector2> spacing { get; private set; }
        public DataObserver<Vector2> position { get; private set; }
        public DataObserver<bool> isCircle { get; private set; }
        public DataObserver<bool> isStartAwake { get; private set; }
        public DataObserver<bool> isUseState { get; private set; }
        public DataObserver<int> amount { get; private set; }
        private List<BulletStateDataObserver> stateList;

        public BulletDataObserver()
        {
            id = UNDEFINED_ID;
            name = new();
            size = new();
            speed = new();
            timeCooldown = new();
            spinSpeed = new();
            lifetime = new();
            unitAngle = new();
            startAngle = new();
            spacing = new();
            position = new();
            amount = new();
            isCircle = new();
            isStartAwake = new();
            isUseState = new();
            stateList = new();
            size.SetData(1);
            speed.SetData(2.5f);
            timeCooldown.SetData(2);
            amount.SetData(1);
            lifetime.SetData(7.5f);
        }
        public BulletDataObserver(EnemyBulletMetaData bulletData) : this() => ImportData(bulletData);
        public BulletDataObserver Clone()
        {
            BulletDataObserver newData = new();
            newData.name.SetData(name.data + " clone");
            newData.size.SetData(size.data);
            newData.speed.SetData(speed.data);
            newData.timeCooldown.SetData(timeCooldown.data);
            newData.spinSpeed.SetData(spinSpeed.data);
            newData.lifetime.SetData(lifetime.data);
            newData.unitAngle.SetData(unitAngle.data);
            newData.startAngle.SetData(startAngle.data);
            newData.spacing.SetData(spacing.data);
            newData.position.SetData(position.data);
            newData.amount.SetData(amount.data);
            newData.isStartAwake.SetData(isStartAwake.data);
            newData.isCircle.SetData(isCircle.data);
            newData.isUseState.SetData(isUseState.data);
            for (int i = 0; i < stateList.Count; i++)
                newData.stateList.Add(stateList[i].Clone());
            return newData;
        }
        public EnemyBulletMetaData ExportData()
        {
            EnemyBulletMetaData bulletData = new()
            {
                id = id,
                name = name.data,
                size = size.data,
                speed = speed.data,
                timeCooldown = timeCooldown.data,
                spacing = new(spacing.data),
                position = new(position.data),
                spinSpeed = spinSpeed.data,
                lifetime = lifetime.data,
                unitAngle = unitAngle.data,
                startAngle = startAngle.data,
                isCircle = isCircle.data,
                isStartAwake = isStartAwake.data,
                isUseState = isUseState.data,
                amount = amount.data,
                states = new EnemyBulletMetaData.BulletStateData[stateList.Count],
            };
            for (int i = 0; i < stateList.Count; i++)
                bulletData.states[i] = stateList[i].ExportData();
            return bulletData;
        }
        public void ImportData(EnemyBulletMetaData data)
        {
            id = data.id;
            name.SetData(data.name);
            size.SetData(data.size);
            speed.SetData(data.speed);
            timeCooldown.SetData(data.timeCooldown);
            spacing.SetData(data.spacing.ToVector2());
            position.SetData(data.position.ToVector2());
            spinSpeed.SetData(data.spinSpeed);
            lifetime.SetData(data.lifetime);
            unitAngle.SetData(data.unitAngle);
            startAngle.SetData(data.startAngle);
            isCircle.SetData(data.isCircle);
            isStartAwake.SetData(data.isStartAwake);
            isUseState.SetData(data.isUseState);
            amount.SetData(data.amount);
            if (data.states != null && data.states.Length > 0)
                for (int i = 0; i < data.states.Length; i++)
                    stateList.Add(new(data.states[i]));
        }
        public void GetList(out List<BulletStateDataObserver> list) => list = stateList;
        public void CreateEmpty(out BulletStateDataObserver data)
        {
            data = new();
            Add(data);
        }
        public void Add(BulletStateDataObserver data) => stateList.Add(data);
        public void Remove(BulletStateDataObserver data) => stateList.Remove(data);
        public void Remove(int index, out BulletStateDataObserver data)
        {
            data = stateList[index];
            stateList.RemoveAt(index);
        }
        public void Set(int index, BulletStateDataObserver data)
            => stateList[index] = data;
    }
}