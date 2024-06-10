# MauiAndroidKeyboard

Maui.Android/iOS Entry Handler 를 사용한 SoftKeyboard Hide/Show.
Maui에서 기본으로 제공하는 IsSoftInputShowing/HideSoftInputAsync/ShowSoftInputAsync 를 사용할 경우
키보드가 보였다가 사라지는 문제가 있음. 우리가 원하는 목적은 처음부터 Entry에 SoftKeyboard가 보이지 않게 해야함.
사용 목적 : Android PDA의 바코드 스캐너에서 Entry에 SoftKeyboard가 보이지 않게 하기 위함.
           (블루투스 바코드 스캐너를 사용하는 Android/iOS에도 필요함)

\Views\HandlerEntryView2.xaml 이 화면의 소스를 참고.

iOS의 Handler도 개발 소스 추가.
\iOS\Handlers\CustomEntryHandler2.cs

테스트 영상(test movie) : keyboard2.mp4


https://github.com/pulmuone/MauiAndroidKeyboard/assets/42885949/64e70161-715f-4c92-b938-0929041d3eed



SoftKeyboard Hide/Show using Maui.Android/iOS Entry Handler.
When using IsSoftInputShowing/HideSoftInputAsync/ShowSoftInputAsync provided by default in Maui
There is an issue where the keyboard appears and then disappears. Our goal is to make the SoftKeyboard invisible in Entry from the beginning.
Purpose of use: To prevent the SoftKeyboard from appearing in Entry in the Android PDA's barcode scanner.
 (Also required for Android/iOS using a Bluetooth barcode scanner)

\Views\HandlerEntryView2.xaml Refer to the source of this screen.

iOS Handler has also been added as a development source.
\iOS\Handlers\CustomEntryHandler2.cs
