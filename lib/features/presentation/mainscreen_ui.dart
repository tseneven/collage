import 'dart:async';

import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:olimp_flutter/features/presentation/contact_widget.dart';
import 'package:olimp_flutter/features/presentation/footer.dart';
import 'package:olimp_flutter/features/presentation/form_widget.dart';
import 'package:olimp_flutter/features/presentation/reviews_widget.dart';
import 'package:olimp_flutter/features/presentation/header.dart';
import 'package:olimp_flutter/features/presentation/whyme_widget.dart';

class MainScreen extends StatefulWidget {
  const MainScreen({super.key});

  @override
  State<MainScreen> createState() => _MainScreenState();
}

class _MainScreenState extends State<MainScreen> {
  final List<String> words = [
    "Озеро",
    "Сибирь",
    "Легенда",
    "Лёденная сказка",
    "Глубина",
    "Хребет",
    "Листвянка",
    "Байкальский нерпа",
    "Прозрачность",
    "Прибой",
  ];

  int currentIndex = 0;
  Timer? timer;
  bool isHovering = false;
  ScrollController controller = ScrollController();

  @override
  void initState() {
    super.initState();
    timer = Timer.periodic(const Duration(seconds: 3), (Timer t) {
      if (!mounted) return;
      setState(() {
        currentIndex = (currentIndex + 1) % words.length;
      });
    });
  }

  @override
  void dispose() {
    timer?.cancel();
    controller.dispose();
    super.dispose();  
  }

      void scrollToFactor(double factor) {
    double h = MediaQuery.of(context).size.height;

    controller.animateTo(
      h * factor,
      duration: const Duration(milliseconds: 600),
      curve: Curves.easeInOut,
    );
  }


  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
        children: [
          SingleChildScrollView(
            controller: controller,
            child: Column(
              children: [
                Container(
                  height: 2000,
                  width: MediaQuery.of(context).size.width,
                  decoration: BoxDecoration(
                    image: DecorationImage(
                      image: AssetImage('fon.jpg'),
                      fit: BoxFit.cover,
                    ),
                  ),
                  child: Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 50.0),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        SizedBox(height: 210),
                        FittedBox(
                          child: Text(
                            "БАЙКАЛ",
                            style: GoogleFonts.ubuntu(
                              color: Colors.white,
                              fontSize: 200,
                              fontWeight: FontWeight.bold,
                              decoration: TextDecoration.none,
                            ),
                          ),
                        ),
                        Padding(
                          padding: const EdgeInsets.only(left: 15.0),
                          child: FittedBox(
                            child: Text(
                              "Погрузитесь во все тайны сибирской сказки",
                              style: GoogleFonts.ubuntu(
                                color: Colors.white,
                                fontSize: 50,
                                decoration: TextDecoration.none,
                              ),
                            ),
                          ),
                        ),
                        Padding(
                          padding: const EdgeInsets.only(left: 15.0),
                          child: AnimatedSwitcher(
                            duration: const Duration(milliseconds: 600),
                            transitionBuilder: (child, animation) {
                              final offsetAnimation = Tween<Offset>(
                                begin: const Offset(0, 0.5),
                                end: Offset.zero,
                              ).animate(animation);

                              return FadeTransition(
                                opacity: animation,
                                child: SlideTransition(
                                  position: offsetAnimation,
                                  child: child,
                                ),
                              );
                            },
                            child: Text(
                              words[currentIndex],
                              key: ValueKey<String>(words[currentIndex]),
                              style: GoogleFonts.ubuntu(
                                color: Colors.white,
                                fontSize: 50,
                                fontWeight: FontWeight.w100,
                                decoration: TextDecoration.none,
                              ),
                            ),
                          ),
                        ),
                        SizedBox(height: 70),
                        Center(
                          child: MouseRegion(
                            onEnter: (_) => setState(() => isHovering = true),
                            onExit: (_) => setState(() => isHovering = false),
                            child: GestureDetector(
                              onTap: () => scrollToFactor(1.1),
                              child: Container(
                                decoration: BoxDecoration(
                                  boxShadow: [
                                    BoxShadow(
                                      color: Colors.black.withOpacity(
                                        isHovering ? 0.5 : 0.25,
                                      ),
                                      blurRadius: 10,
                                      offset: Offset(0, 4),
                                    ),
                                  ],
                                  border: Border.all(
                                    color: Colors.white,
                                    width: 3,
                                  ),
                                ),
                                child: Padding(
                                  padding: const EdgeInsets.symmetric(
                                    horizontal: 30.0,
                                    vertical: 12.0,
                                  ),
                                  child: Text(
                                    "Почему тебе нужен\nименно наш тур?",
                                    style: GoogleFonts.ubuntu(
                                      color: Colors.white,
                                      fontSize: 20,
                                      fontWeight: FontWeight.w300,
                                      decoration: TextDecoration.none,
                                    ),
                                  ),
                                ),
                              ),
                            ),
                          ),
                        ),
                        SizedBox(height: 500),
                        Center(
                          child: FittedBox(
                            child: Text(
                              "Почему мы?",
                              style: GoogleFonts.ubuntu(
                                color: Colors.white,
                                fontSize: 70,
                                fontWeight: FontWeight.bold,
                                decoration: TextDecoration.none,
                              ),
                            ),
                          ),
                        ),
                        WhyMeWidget(),
                      ],
                    ),
                  ),
                ),
                ReviewsWidget(),
                FormWidget(),
                ContactWidget(),
                FooterWidget(scrollController: controller,)
              ],
            ),
          ),
          Positioned(top: 0, left: 0, right: 0, child: Header(scrollController: controller,)),
        ],
      ),
    );
  }
}


