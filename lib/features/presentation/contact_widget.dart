import 'package:flutter/material.dart';
// ignore: avoid_web_libraries_in_flutter
import 'dart:ui_web' as ui;
// ignore: avoid_web_libraries_in_flutter
import 'dart:html';

import 'package:google_fonts/google_fonts.dart';

class ContactWidget extends StatefulWidget {
  const ContactWidget({super.key});

  @override
  State<ContactWidget> createState() => _ContactWidgetState();
}

class _ContactWidgetState extends State<ContactWidget> {
  @override
  void initState() {
    super.initState();

    ui.platformViewRegistry.registerViewFactory(
      'baikal-map',
      (int viewId) => IFrameElement()
        ..src =
            'https://yandex.ru/map-widget/v1/?ll=104.339769%2C52.257907&z=16&pt=104.339769,52.257907,pm2rdl'
        ..style.border = '0'
        ..style.width = '100%'
        ..style.height = '100%',
    );
  }

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (context, constraints) {
        bool isMobile = constraints.maxWidth < 800;

        double mapWidth = isMobile
            ? constraints.maxWidth
            : constraints.maxWidth * 0.4;

        return Container(
          height: 1000,
          decoration: const BoxDecoration(
            image: DecorationImage(
              image: AssetImage('fon2.jpg'),
              fit: BoxFit.cover,
            ),
          ),
          child: isMobile
              ? Padding(
                  padding: const EdgeInsets.symmetric(
                    horizontal: 20.0,
                    vertical: 20.0,
                  ),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Container(
                        height: 400,
                        width: mapWidth,
                        decoration: BoxDecoration(
                          borderRadius: BorderRadius.circular(20),
                        ),
                        clipBehavior: Clip.hardEdge,
                        child: const HtmlElementView(viewType: 'baikal-map'),
                      ),
                      _ContactWidget(mapWidth: mapWidth),
                    ],
                  ),
                )
              : Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 50.0),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Container(
                        height: 500,
                        width: mapWidth,
                        decoration: BoxDecoration(
                          borderRadius: BorderRadius.circular(20),
                        ),
                        clipBehavior: Clip.hardEdge,
                        child: const HtmlElementView(viewType: 'baikal-map'),
                      ),
                      _ContactWidget(mapWidth: mapWidth),
                    ],
                  ),
                ),
        );
      },
    );
  }
}

class _ContactWidget extends StatelessWidget {
  const _ContactWidget({required this.mapWidth});

  final double mapWidth;

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 550,
      width: mapWidth,
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(20),
        color: Color(0xFF24314A),
      ),
      clipBehavior: Clip.hardEdge,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          SizedBox(height: 30),
          Center(
            child: Text(
              "Контакты",
              style: GoogleFonts.ubuntu(
                color: Colors.white,
                fontSize: 50,
                decoration: TextDecoration.none,
              ),
            ),
          ),
          SizedBox(height: 50),
          Padding(
            padding: const EdgeInsets.only(left: 40.0),
            child: Text(
              "ООО «Арктическое приключениe»\nг. Иркутск, ул. Байкальская, 217, офис 403\nИНН: 5529017432\nОГРН: 1293850042117\nТелефон:\n+7 (3952) 55-48-90\n+7 (924) 010-77-33",
              style: GoogleFonts.ubuntu(
                color: Colors.white,
                fontSize: 22,
                decoration: TextDecoration.none,
              ),
            ),
          ),
          SizedBox(height: 30),
          Padding(
            padding: const EdgeInsets.only(left: 40.0),
            child: Row(
              children: [
                GestureDetector(
                  child: Container(
                    width: 50,
                    height: 50,
                    decoration: BoxDecoration(
                      image: DecorationImage(image: AssetImage('tg.png')),
                      color: Colors.white,
                      borderRadius: BorderRadius.circular(100),
                    ),
                  ),
                ),
                SizedBox(width: 50),
                GestureDetector(
                  child: Container(
                    width: 50,
                    height: 50,
                    decoration: BoxDecoration(
                      image: DecorationImage(
                        image: AssetImage('vk.png'),
                        fit: BoxFit.cover,
                      ),
                      color: Colors.white,
                      borderRadius: BorderRadius.circular(100),
                    ),
                  ),
                ),
                SizedBox(width: 50),
                GestureDetector(
                  child: Container(
                    width: 50,
                    height: 50,
                    decoration: BoxDecoration(
                      image: DecorationImage(image: AssetImage('outlook.png')),
                      color: Colors.white,
                      borderRadius: BorderRadius.circular(100),
                    ),
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
