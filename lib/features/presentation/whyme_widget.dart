import 'dart:async';

import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:smooth_page_indicator/smooth_page_indicator.dart';

class WhyMeWidget extends StatefulWidget {
  const WhyMeWidget({super.key});
  @override
  State<WhyMeWidget> createState() => _WhyMeWidgetState();
}

class _WhyMeWidgetState extends State<WhyMeWidget> {
  final PageController controller = PageController();
  int pageIndex = 0;

  final List<Map<String, String>> slides = [
    {
      "title": "Опытные гиды",
      "text":
          "Наши проводники знают Байкал так, как никто другой.\n"
          "Вы узнаете не только историю, но и тайные места,\n"
          "недоступные обычным туристам.",
    },
    {
      "title": "Комфорт и безопасность",
      "text":
          "Мы используем только проверенные маршруты,\n"
          "тёплое современное снаряжение и комфортные базы отдыха.\n"
          "Вы наслаждаетесь природой — остальное мы берём на себя.",
    },
    {
      "title": "Уникальные впечатления",
      "text":
          "Замёрзшие торосы, прозрачный лёд, нерпы, закаты,\n"
          "тёплое общение и атмосфера настоящей сибирской сказки.\n"
          "Это путешествие вы запомните на всю жизнь.",
    },
  ];

  late Timer _timer;

  @override
  void initState() {
    super.initState();

    _timer = Timer.periodic(const Duration(seconds: 4), (timer) {
      if (!mounted) return;

      pageIndex = (pageIndex + 1) % slides.length;

      controller.animateToPage(
        pageIndex,
        duration: const Duration(milliseconds: 500),
        curve: Curves.easeInOut,
      );
    });
  }

  @override
  void dispose() {
    _timer.cancel();
    super.dispose();
    controller.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final width = MediaQuery.of(context).size.width;
    final isMobile = width < 700;

    return Column(
      children: [
        SizedBox(
          height: isMobile ? 250 : 260,
          child: PageView.builder(
            controller: controller,
            itemCount: slides.length,
            itemBuilder: (context, index) {
              final item = slides[index];

              return Padding(
                padding: const EdgeInsets.symmetric(horizontal: 40.0),
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Text(
                      item["title"]!,
                      textAlign: TextAlign.center,
                      style: GoogleFonts.ubuntu(
                        fontSize: isMobile ? 28 : 34,
                        color: Colors.white,
                        fontWeight: FontWeight.bold,
                        decoration: TextDecoration.none,
                      ),
                    ),
                    const SizedBox(height: 20),
                    Text(
                      item["text"]!,
                      textAlign: TextAlign.center,
                      style: GoogleFonts.ubuntu(
                        fontSize: isMobile ? 18 : 22,
                        color: Colors.white70,
                        fontWeight: FontWeight.w300,
                        height: 1.4,
                        decoration: TextDecoration.none,
                      ),
                    ),
                  ],
                ),
              );
            },
          ),
        ),
        const SizedBox(height: 30),
        SmoothPageIndicator(
          controller: controller,
          count: slides.length,
          effect: WormEffect(
            spacing: 12,
            dotHeight: 12,
            dotWidth: 12,
            activeDotColor: Colors.white,
            dotColor: Colors.white38,
          ),
        ),
      ],
    );
  }
}
