🌐 R_WEB_PROJECT
ASP.NET Core 7.0으로 만든 웹 애플리케이션입니다. JWT 인증, Redis 세션 관리 등을 공부하면서 만들었습니다.

⚠️ 프로젝트 상태
현재는 개발을 중단한 상태입니다.
처음에는 제대로 된 엔터프라이즈급 웹 애플리케이션을 만들어보고 싶어서 시작했는데, 로그인/인증 부분까지 구현하고 나서 다른 프로젝트로 넘어가게 되었습니다. 
그래도 ASP.NET Core로 아키텍처 구조를 만들어본 경험이라 의미있다고 생각해서 정리해둡니다.

🎯 뭘 만들려고 했나
  - JWT 기반 로그인/인증 시스템
  - Redis로 세션 관리 (여러 서버에서 돌릴 수 있게)
  - Controller-Service-Repository 계층 구조
  - Dapper랑 Entity Framework 같이 사용
  - Log4net으로 로깅
  - 개발/운영 환경 분리

📚 공부한 것들
✅ 제대로 구현한 것
  - ASP.NET Core MVC 구조 - Controller, Service, Repository 분리해서 만들었습니다
  - JWT 인증 - 토큰 생성, 검증 로직 직접 구현했습니다
  - 비밀번호 보안 - Salt 써서 해싱하는 것도 공부했습니다
  - Redis 세션 - 분산 환경 대비해서 Redis에 세션 저장하도록 했습니다
  - Dapper + EF Core - 상황에 맞게 ORM 선택해서 쓰는 방법 배웠습니다
  - 의존성 주입 - ASP.NET Core DI 컨테이너 제대로 활용해봤습니다

❌ 못한 것
  - Main 페이지 기능들 (로그인만 만들고 끝...)
  - 프론트엔드 디자인 (완전 기본 HTML만 있음)
  - 테스트 코드 (시간 없어서 못함)
  - API 문서 (Swagger 붙이려다 말았음)
  - 배포 (로컬에서만 돌려봄)
