import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class ReviewsWidget extends StatefulWidget {
  const ReviewsWidget({super.key});

  @override
  State<ReviewsWidget> createState() => _ReviewsWidgetState();
}

class _ReviewsWidgetState extends State<ReviewsWidget> {
  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (context, constraints) {
        double width = constraints.maxWidth;

        double cardWidth = width > 1400
            ? 450
            : width > 1000
            ? width / 4 - 100
            : width - 100;
        return Container(
          decoration: BoxDecoration(color: Color.fromARGB(255, 2, 10, 14)),
          child: Column(
            children: [
              SizedBox(height: 100),
              Center(
                child: FittedBox(
                  child: Text(
                    "Отзывы",
                    style: GoogleFonts.ubuntu(
                      color: Colors.white,
                      fontSize: 70,
                      fontWeight: FontWeight.bold,
                      decoration: TextDecoration.none,
                    ),
                  ),
                ),
              ),
              SizedBox(height: 200),
              Wrap(
                spacing: 10,
                runSpacing: 6,
                children: [
                  ReviewCard(
                    title: "Мария, 27 лет (Москва)",
                    description:
                        "Не ожидала, что Байкал окажется таким живым. Лёд под ногами буквально поёт! Организация тура — на высоте: тёплые домики, вкусная еда, продуманные маршруты. Отдельно спасибо гиду — рассказал столько историй, что я влюбилась в эти места окончательно.",
                    face: 'face2.png',
                    width: cardWidth,
                  ),
                  ReviewCard(
                    title: "Антон, 31 год (Екатеринбург)",
                    description:
                        "Хочу сказать огромное спасибо за этот тур. Очень редко встретишь команду, которая искренне любит своё дело. Показали не только “открыточные” места, но и такие уголки Байкала, куда сам бы никогда не дошёл. Фотографии — космос!",
                    face: 'face1.png',
                    width: cardWidth,
                  ),
                  ReviewCard(
                    title: "Ольга и Артём, 35 и 38 лет (Новосибирск)",
                    description:
                        "Путешествовали вдвоём — хотели перезагрузиться, и это получилось на 100%. Всё спокойное, уютное, атмосферное. Особенно понравилась прогулка по ледяным гротам — как будто попали в другой мир. Спасибо за комфорт, внимание и безопасность на каждом шагу.",
                    face: 'face4.png',
                    width: cardWidth,
                  ),
                  ReviewCard(
                    title: "Виктор, 24 года",
                    description:
                        "Если хотите увидеть Байкал по-настоящему, а не просто “как на картинке”, берите этот тур. Маршрут сбалансированный: адреналин там, где надо, и тишина там, где хочется остановиться. Понравилось, что группа маленькая — ничто не напрягало.",
                    face: 'face3.png',
                    width: cardWidth,
                  ),
                ],
              ),
              SizedBox(height: 100),
              Divider(
                color: Colors.white,
                thickness: 1,
                indent: 100,
                endIndent: 100,
              ),
              SizedBox(height: 100),
            ],
          ),
        );
      },
    );
  }
}

class ReviewCard extends StatefulWidget {
  final String title;
  final String description;
  final String face;
  final double width;

  const ReviewCard({
    super.key,
    required this.title,
    required this.description,
    required this.face,
    required this.width,
  });

  @override
  State<ReviewCard> createState() => _ReviewCardState();
}

class _ReviewCardState extends State<ReviewCard> {
  bool isHovering = false;

  @override
  Widget build(BuildContext context) {
    return MouseRegion(
      onEnter: (_) => setState(() => isHovering = true),
      onExit: (_) => setState(() => isHovering = false),
      child: AnimatedContainer(
        duration: Duration(milliseconds: 300),
        transform: isHovering
            ? Matrix4.translationValues(0, -10, 0)
            : Matrix4.identity(),
        width: widget.width,
        padding: EdgeInsets.all(20),
        margin: EdgeInsets.symmetric(vertical: 10),
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(20),
          gradient: LinearGradient(
            colors: isHovering
                ? [Color(0xFF3A4A6B), Color(0xFF24314A)]
                : [Color(0xFF2E3A59), Color(0xFF1C2536)],
            begin: Alignment.topLeft,
            end: Alignment.bottomRight,
          ),
          boxShadow: [
            BoxShadow(
              color: Colors.black.withOpacity(isHovering ? 0.5 : 0.3),
              blurRadius: 20,
              offset: Offset(0, 8),
            ),
          ],
        ),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            FittedBox(
              child: Row(
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  CircleAvatar(
                    backgroundImage: AssetImage(widget.face),
                    radius: 40,
                  ),
                  SizedBox(width: 15),
                  Text(
                    widget.title,
                    style: GoogleFonts.ubuntu(
                      fontSize: 22,
                      fontWeight: FontWeight.w600,
                      color: Colors.white,
                      decoration: TextDecoration.none,
                    ),
                  ),
                ],
              ),
            ),
            SizedBox(height: 20),
            Text(
              widget.description,
              style: GoogleFonts.ubuntu(
                fontSize: 18,
                color: Colors.white70,
                height: 1.5,
                decoration: TextDecoration.none,
              ),
              textAlign: TextAlign.justify,
            ),
            SizedBox(height: 20),
            Row(
              children: List.generate(
                5,
                (index) =>
                    Icon(Icons.star, color: Colors.amberAccent, size: 20),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
