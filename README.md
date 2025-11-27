# Олимпиада по Web Разработке (Flutter / Flutter Web)

Репозиторий: https://github.com/tseneven/OlimpKuznetsovAA_Pr-33_Flutter

## Описание

Проект — адаптивная посадочная страница (landing page), реализованная на Flutter с поддержкой Flutter Web. Тема — туристический продукт «Арктическое приключение» (тур на Байкал). Цель — показать структуру и интерактивные блоки, требуемые в задании: приветственный экран, преимущества, отзывы, форма обратной связи, контакты, навигация и футер.

## Что реализовано (соответствие заданию)

В landing реализованы все требуемые блоки и интерактивы:

Приветственный экран

Большой титул, анимированная строка-слова (AnimatedSwitcher), CTA-кнопка, плавный скролл к разделам.

Блок преимуществ

Слайдер / PageView с преимуществами, автоматическое перелистывание, hover-эффекты.

Блок с отзывами

Карточки отзывов (Wrap) с hover-анимацией и адаптивным расположением.

Блок обратной связи

Форма с полями (имя, телефон) и кнопкой; вёрстка адаптируется под разные экраны.

Блок контактов

Карта (Яндекс) в iframe для Web, контактные данные, иконки соцсетей (с адаптивным Wrap).

Меню

Header с адаптивным меню (desktop — пункты, mobile — PopupMenu), клики прокручивают страницу (ScrollController).

Футер

Дублирующая контактная информация, меню, соцсети. Элементы реагируют на нажатия и не выходят за пределы (используется Wrap).

Интерактивные элементы

Анимированная смена слов (Timer + AnimatedSwitcher).

Hover-эффекты (MouseRegion + AnimatedContainer) для карточек и кнопок (desktop).

Пагинация/слайдер (PageView + SmoothPageIndicator).

Плавный скролл к секциям (ScrollController.animateTo).

Встраивание Яндекс.Карт для Web через HtmlElementView / iframe.

Попап-меню на мобильных (PopupMenuButton).

## Технологии и зависимости

Flutter (>= 3.x) — проект ориентирован на Flutter Web.

## Пакеты:

google_fonts

smooth_page_indicator

## Требования / Примечания

Проект ориентирован на Web; некоторые части (встраивание dart:html, platformViewRegistry) работают только в Flutter Web.

Убедитесь, что все ассеты перечислены в pubspec.yaml (фоновые изображения, логотипы, иконки, фотографии для отзывов).

Если запускаете не на Web — часть функционала с HtmlElementView не доступна.

## Как запустить (локально)

Установите релиз:

https://github.com/tseneven/OlimpKuznetsovAA_Pr-33_Flutter/releases/tag/release

Разархивируйте.

Запустите через VSCode и [LiveServer](https://marketplace.visualstudio.com/items?itemName=ritwickdey.LiveServer)

Или serve

```
npm install -g serve
serve
```

Структура проекта (основные файлы)
```
lib
│   main.dart 
│
│───features
│   └───presentation
│       contact_widget.dart        # Карта и контакты (Web iframe)
│       footer.dart                # Футер
│       form_widget.dart           # Форма обратной связи
│       header.dart                # Хедер/меню + прокрутка
│       mainscreen_ui.dart         # Главный экран (компоновка блоков)
│       reviews_widget.dart        # Блок отзывов
│       whyme_widget.dart          # Блок "Почему мы"
pubspec.yaml                       # Зависимости
assets                             # Ассеты
```

           
