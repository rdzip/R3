#if R3_UGUI_SUPPORT
using R3;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace R3
{
    public static partial class UnityUIComponentExtensions
    {
        static void OnNext(string x, Text t) => t.text = x;
        static void OnNext1<T>(T x, Text t) => t.text = x.ToString();
        static void OnNext2<T>(T x, (Text text, Func<T, string> selector) state) => state.text.text = state.selector(x);
        static void OnNext3(bool x, Selectable s) => s.interactable = x;

        static IDisposable Subscribe(Observer<bool> observer, Toggle t)
        {
            observer.OnNext(t.isOn);
            return t.onValueChanged.AsObservable(t.GetDestroyCancellationToken()).Subscribe(observer);
        }
        static IDisposable Subscribe2(Observer<float> observer, Scrollbar s)
        {
            observer.OnNext(s.value);
            return s.onValueChanged.AsObservable(s.GetDestroyCancellationToken()).Subscribe(observer);
        }
        static IDisposable Subscribe3(Observer<Vector2> observer, ScrollRect s)
        {
            observer.OnNext(s.normalizedPosition);
            return s.onValueChanged.AsObservable(s.GetDestroyCancellationToken()).Subscribe(observer);
        }
        static IDisposable Subscribe4(Observer<float> observer, Slider s)
        {
            observer.OnNext(s.value);
            return s.onValueChanged.AsObservable(s.GetDestroyCancellationToken()).Subscribe(observer);
        }
        static IDisposable Subscribe5(Observer<string> observer, InputField i)
        {
            observer.OnNext(i.text);
            return i.onValueChanged.AsObservable(i.GetDestroyCancellationToken()).Subscribe(observer);
        }
        static IDisposable Subscribe6(Observer<int> observer, Dropdown d)
        {
            observer.OnNext(d.value);
            return d.onValueChanged.AsObservable(d.GetDestroyCancellationToken()).Subscribe(observer);
        }

        public static IDisposable SubscribeToText(this Observable<string> source, Text text)
        {
            return source.Subscribe(text, OnNext);
        }

        public static IDisposable SubscribeToText<T>(this Observable<T> source, Text text)
        {
            return source.Subscribe(text, OnNext1);
        }

        public static IDisposable SubscribeToText<T>(this Observable<T> source, Text text, Func<T, string> selector)
        {
            return source.Subscribe((text, selector), OnNext2);
        }

        public static IDisposable SubscribeToInteractable(this Observable<bool> source, Selectable selectable)
        {
            return source.Subscribe(selectable, OnNext3);
        }

        /// <summary>Observe onClick event.</summary>
        public static Observable<Unit> OnClickAsObservable(this Button button)
        {
            return button.onClick.AsObservable(button.GetDestroyCancellationToken());
        }

        /// <summary>Observe onValueChanged with current `isOn` value on subscribe.</summary>
        public static Observable<bool> OnValueChangedAsObservable(this Toggle toggle)
        {
            // Optimized Defer + StartWith

            return Observable.Create<bool, Toggle>(toggle, Subscribe);
        }

        /// <summary>Observe onValueChanged with current `value` on subscribe.</summary>
        public static Observable<float> OnValueChangedAsObservable(this Scrollbar scrollbar)
        {

            return Observable.Create<float, Scrollbar>(scrollbar, Subscribe2);
        }

        /// <summary>Observe onValueChanged with current `normalizedPosition` value on subscribe.</summary>
        public static Observable<Vector2> OnValueChangedAsObservable(this ScrollRect scrollRect)
        {

            return Observable.Create<Vector2, ScrollRect>(scrollRect, Subscribe3);
        }

        /// <summary>Observe onValueChanged with current `value` on subscribe.</summary>
        public static Observable<float> OnValueChangedAsObservable(this Slider slider)
        {

            return Observable.Create<float, Slider>(slider, Subscribe4);
        }

        /// <summary>Observe onEndEdit(Submit) event.</summary>
        public static Observable<string> OnEndEditAsObservable(this InputField inputField)
        {
            return inputField.onEndEdit.AsObservable(inputField.GetDestroyCancellationToken());
        }

        /// <summary>Observe onValueChanged with current `text` value on subscribe.</summary>
        public static Observable<string> OnValueChangedAsObservable(this InputField inputField)
        {

            return Observable.Create<string, InputField>(inputField, Subscribe5);
        }

        /// <summary>Observe onValueChanged with current `value` on subscribe.</summary>
        public static Observable<int> OnValueChangedAsObservable(this Dropdown dropdown)
        {

            return Observable.Create<int, Dropdown>(dropdown, Subscribe6);
        }
    }
}
#endif
