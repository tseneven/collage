import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class FormWidget extends StatelessWidget {
  const FormWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.only(top: 20),
      decoration: const BoxDecoration(color: Color.fromARGB(255, 2, 10, 14)),
      child: LayoutBuilder(
        builder: (context, constraints) {
          double maxWidth = constraints.maxWidth;

          bool isMobile = maxWidth < 1235;

          double formWidth = maxWidth > 1200
              ? maxWidth * 0.4
              : maxWidth > 800
              ? maxWidth * 0.6
              : maxWidth * 0.9;

          double titleSize = maxWidth > 1000 ? 70 : (maxWidth > 600 ? 45 : 32);
          double subtitleSize = maxWidth > 600 ? 20 : 16;

          double horizontalPadding = maxWidth > 1000 ? 70 : 20;

          return Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Padding(
                padding: EdgeInsets.only(left: horizontalPadding),
                child: Text(
                  "Остались вопросы?",
                  style: GoogleFonts.ubuntu(
                    color: Colors.white,
                    fontSize: titleSize,
                    fontWeight: FontWeight.bold,
                    decoration: TextDecoration.none,
                  ),
                ),
              ),
              Padding(
                padding: EdgeInsets.only(left: horizontalPadding + 8, top: 10),
                child: SizedBox(
                  width: formWidth + 100,
                  child: Text(
                    "Заполните форму и наши менеджеры с вами свяжуться в ближайшее время!",
                    style: GoogleFonts.ubuntu(
                      color: Colors.white,
                      fontSize: subtitleSize,
                      fontWeight: FontWeight.w100,
                      decoration: TextDecoration.none,
                    ),
                  ),
                ),
              ),
              const SizedBox(height: 60),

              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Padding(
                    padding: EdgeInsets.only(left: horizontalPadding + 8, bottom: isMobile ? 100 : 0),
                    child: Container(
                      width: formWidth,
                      decoration: BoxDecoration(
                        border: Border.all(width: 2, color: Colors.white),
                        borderRadius: BorderRadius.circular(20),
                      ),
                      child: Padding(
                        padding: const EdgeInsets.symmetric(
                          horizontal: 40.0,
                          vertical: 20.0,
                        ),
                        child: Column(
                          children: [
                            Padding(
                              padding: EdgeInsets.symmetric(vertical: 30.0),
                              child: _InputWidget(text: "Имя"),
                            ),
                            Padding(
                              padding: EdgeInsets.symmetric(vertical: 30.0),
                              child: _InputWidget(text: "Номер телефона"),
                            ),
                            Padding(
                              padding: EdgeInsets.symmetric(vertical: 30.0),
                              child: GestureDetector(
                                child: Container(
                                  height: 60,
                                  decoration: BoxDecoration(
                                    color: Color(0xFF24314A),
                                    borderRadius: BorderRadius.circular(10),
                                  ),
                                  child: Center(
                                    child: Text(
                                      "Отправить",
                                      style: GoogleFonts.ubuntu(
                                        color: Colors.white,
                                        fontSize: subtitleSize,
                                        fontWeight: FontWeight.w400,
                                      ),
                                    ),
                                  ),
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                    ),
                  ),
                  if (!isMobile)
                    Column(
                      mainAxisAlignment: MainAxisAlignment.end,
                      children: [Image.asset('nerp.png')],
                    ),
                ],
              ),
            ],
          );
        },
      ),
    );
  }
}

class _InputWidget extends StatelessWidget {
  final String text;

  const _InputWidget({required this.text});

  @override
  Widget build(BuildContext context) {
    return TextField(
      style: const TextStyle(color: Colors.white),
      decoration: InputDecoration(
        filled: true,
        fillColor: Colors.white,
        hintText: text,
        hintStyle: GoogleFonts.ubuntu(color: Colors.grey, fontSize: 18),
        contentPadding: const EdgeInsets.symmetric(
          horizontal: 20,
          vertical: 22,
        ),
        border: OutlineInputBorder(borderRadius: BorderRadius.circular(10)),
        enabledBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(10),
        ),
        focusedBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(10),
          borderSide: const BorderSide(color: Colors.blueAccent, width: 2),
        ),
      ),
    );
  }
}
