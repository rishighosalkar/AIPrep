from fastapi import FastAPI
from app.routes import questions

app = FastAPI(
    title="AIPrep NLP service",
    version="1.0"
)

app.include_router(questions.router,prefix="/api/questions")