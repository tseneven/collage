import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class Header extends StatefulWidget {
  final ScrollController scrollController;
  const Header({super.key, required this.scrollController});

  @override
  State<Header> createState() => _HeaderState();
}

class _HeaderState extends State<Header> {
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
    bool isMobile = MediaQuery.of(context).size.width < 900;

    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 10),
      child: isMobile ? _buildMobileHeader() : _buildDesktopHeader(),
    );
  }

  Widget _buildMobileHeader() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Image.asset('logo.png', width: 90),

        PopupMenuButton<String>(
          icon: const Icon(Icons.menu, color: Colors.white, size: 32),
          color: Colors.white,
          onSelected: (value) {
            switch (value) {
              case 'why':
                scrollToFactor(1.0);
                break;
              case 'reviews':
                scrollToFactor(2.4);
                break;
              case 'form':
                scrollToFactor(4.6);
                break;
              case 'contacts':
                scrollToFactor(5.63);
                break;
            }
          },
          itemBuilder: (context) => [
            PopupMenuItem(
              value: 'why',
              child: Text('Почему мы?', style: GoogleFonts.ubuntu()),
            ),
            PopupMenuItem(
              value: 'reviews',
              child: Text('Отзывы', style: GoogleFonts.ubuntu()),
            ),
            PopupMenuItem(
              value: 'form',
              child: Text('Обратная связь', style: GoogleFonts.ubuntu()),
            ),
            PopupMenuItem(
              value: 'contacts',
              child: Text('Контакты', style: GoogleFonts.ubuntu()),
            ),
          ],
        ),
      ],
    );
  }

  Widget _buildDesktopHeader() {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 40.0),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Image.asset('logo.png', width: 120),

          _menuItem('Почему мы?', 1.2),
          _menuItem('Отзывы', 2.4),
          _menuItem('Обратная связь', 3.65),
          _menuItem('Контакты', 4.8),
        ],
      ),
    );
  }

  Widget _menuItem(String text, double scrollValue) {
    return GestureDetector(
      onTap: () => scrollToFactor(scrollValue),
      child: Text(
        text,
        style: GoogleFonts.ubuntu(
          color: Colors.white,
          fontSize: 26,
          decoration: TextDecoration.none,
        ),
      ),
    );
  }
}
