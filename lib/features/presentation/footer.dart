import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class FooterWidget extends StatefulWidget {
  final ScrollController scrollController;
  const FooterWidget({super.key, required this.scrollController});

  @override
  State<FooterWidget> createState() => _FooterWidgetState();
}

class _FooterWidgetState extends State<FooterWidget> {
  void scrollTo(double offset) {
    widget.scrollController.animateTo(
      offset,
      duration: Duration(milliseconds: 600),
      curve: Curves.easeInOut,
    );
  }

  void scrollToFactor(double factor) {
    double h = MediaQuery.of(context).size.height;

    widget.scrollController.animateTo(
      h * factor,
      duration: const Duration(milliseconds: 600),
      curve: Curves.easeInOut,
    );
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      width: MediaQuery.of(context).size.width,
      decoration: BoxDecoration(color: Color.fromARGB(255, 2, 10, 14)),
      child: LayoutBuilder(
        builder: (context, constraints) {
          bool isMobile = constraints.maxWidth < 1235;

          if (isMobile) {
            return Column(
              children: [
                Image.asset('logo.png', width: 200),
                GestureDetector(
                  onTap: () => scrollToFactor(0),
                  child: Text(
                    "Главная\n",
                    style: GoogleFonts.ubuntu(
                      color: Colors.white,
                      fontSize: 22,
                      decoration: TextDecoration.none,
                    ),
                    textAlign: TextAlign.center,
                  ),
                ),
                GestureDetector(
                  onTap: () => scrollToFactor(1.0),
                  child: Text(
                    "Почему мы?\n",
                    style: GoogleFonts.ubuntu(
                      color: Colors.white,
                      fontSize: 22,
                      decoration: TextDecoration.none,
                    ),
                    textAlign: TextAlign.center,
                  ),
                ),
                GestureDetector(
                  onTap: () => scrollToFactor(2.4),
                  child: Text(
                    "Отзывы\n",
                    style: GoogleFonts.ubuntu(
                      color: Colors.white,
                      fontSize: 22,
                      decoration: TextDecoration.none,
                    ),
                    textAlign: TextAlign.center,
                  ),
                ),
                GestureDetector(
                  onTap: () => scrollToFactor(4.6),
                  child: Text(
                    "Обратная связь\n",
                    style: GoogleFonts.ubuntu(
                      color: Colors.white,
                      fontSize: 22,
                      decoration: TextDecoration.none,
                    ),
                    textAlign: TextAlign.center,
                  ),
                ),
                GestureDetector(
                  onTap: () => scrollToFactor(5.63),
                  child: Text(
                    "Контакты\n",
                    style: GoogleFonts.ubuntu(
                      color: Colors.white,
                      fontSize: 22,
                      decoration: TextDecoration.none,
                    ),
                    textAlign: TextAlign.center,
                  ),
                ),
                Text(
                  "ООО «Арктическое приключениe»\n\nг. Иркутск, ул. Байкальская, 217, офис 403\nИНН: 5529017432\nОГРН: 1293850042117\n\nТелефон:\n+7 (3952) 55-48-90\n+7 (924) 010-77-33",
                  style: GoogleFonts.ubuntu(
                    color: Colors.white,
                    fontSize: 22,
                    decoration: TextDecoration.none,
                  ),
                  textAlign: TextAlign.center,
                ),
                SizedBox(height: 50),
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
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
                          image: DecorationImage(
                            image: AssetImage('outlook.png'),
                          ),
                          color: Colors.white,
                          borderRadius: BorderRadius.circular(100),
                        ),
                      ),
                    ),
                  ],
                ),
                SizedBox(height: 150),
              ],
            );
          } else {
            return Padding(
              padding: const EdgeInsets.symmetric(vertical: 50.0),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Image.asset('logo.png', width: 150),
                  GestureDetector(
                    onTap: () => scrollTo(0),
                    child: Text(
                      "Главная\n",
                      style: GoogleFonts.ubuntu(
                        color: Colors.white,
                        fontSize: 18,
                        decoration: TextDecoration.none,
                      ),
                      textAlign: TextAlign.center,
                    ),
                  ),
                  GestureDetector(
                    onTap: () => scrollTo(950),
                    child: Text(
                      "Почему мы?\n",
                      style: GoogleFonts.ubuntu(
                        color: Colors.white,
                        fontSize: 18,
                        decoration: TextDecoration.none,
                      ),
                      textAlign: TextAlign.center,
                    ),
                  ),
                  GestureDetector(
                    onTap: () => scrollTo(2000),
                    child: Text(
                      "Отзывы\n",
                      style: GoogleFonts.ubuntu(
                        color: Colors.white,
                        fontSize: 18,
                        decoration: TextDecoration.none,
                      ),
                      textAlign: TextAlign.center,
                    ),
                  ),
                  GestureDetector(
                    onTap: () => scrollTo(2700),
                    child: Text(
                      "Обратная связь\n",
                      style: GoogleFonts.ubuntu(
                        color: Colors.white,
                        fontSize: 18,
                        decoration: TextDecoration.none,
                      ),
                      textAlign: TextAlign.center,
                    ),
                  ),
                  GestureDetector(
                    onTap: () => scrollTo(3700),
                    child: Text(
                      "Контакты\n",
                      style: GoogleFonts.ubuntu(
                        color: Colors.white,
                        fontSize: 18,
                        decoration: TextDecoration.none,
                      ),
                      textAlign: TextAlign.center,
                    ),
                  ),
                  Text(
                    "ООО «Арктическое приключениe»\n\nг. Иркутск, ул. Байкальская, 217, офис 403\nИНН: 5529017432\nОГРН: 1293850042117\n\nТелефон:\n+7 (3952) 55-48-90\n+7 (924) 010-77-33",
                    style: GoogleFonts.ubuntu(
                      color: Colors.white,
                      fontSize: 18,
                      decoration: TextDecoration.none,
                    ),
                    textAlign: TextAlign.center,
                  ),
                  _socialIcons(),
                ],
              ),
            );
          }
        },
      ),
    );
  }

  Widget _socialIcons() {
    return Wrap(
      spacing: 40,
      runSpacing: 20,
      alignment: WrapAlignment.center,
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
        GestureDetector(
          child: Container(
            width: 50,
            height: 50,
            decoration: BoxDecoration(
              image: DecorationImage(image: AssetImage('vk.png')),
              color: Colors.white,
              borderRadius: BorderRadius.circular(100),
            ),
          ),
        ),
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
    );
  }
}
