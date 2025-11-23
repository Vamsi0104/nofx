import React, { useState } from 'react';
import { createPost } from '../services/api';

const PostForm = () => {
    const [title, setTitle] = useState('');
    const [content, setContent] = useState('');
    const [authorId, setAuthorId] = useState(''); // Assuming this is required for the backend
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!title || !content || !authorId) {
            setError('Title, content, and author ID are required!');
            return;
        }

        setIsSubmitting(true);
        setError(null); // Clear previous errors

        try {
            const newPost = {
                title,
                content,
                authorId,  // Ensure that the authorId is passed
                createdAt: new Date().toISOString(),
                isDeleted: false,
            };

            const response = await createPost(newPost);

            if (response) {
                setTitle('');
                setContent('');
                setAuthorId('');
                alert('Post added successfully!');
            }
        } catch (err) {
            setError('Failed to add post. Please try again.');
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <div className="container-fluid bg-dark text-white min-vh-100">
            <div className="row justify-content-center align-items-center" style={{ height: '100vh' }}>
                <div className="col-12 col-md-8 col-lg-6">
                    <h2 className="mb-4 text-center">Create New Post</h2>
                    {error && <div className="alert alert-danger">{error}</div>}

                    <form onSubmit={handleSubmit} className="bg-dark p-4 rounded shadow-lg">
                        <div className="mb-3">
                            <label htmlFor="title" className="form-label">Title</label>
                            <input
                                id="title"
                                type="text"
                                className="form-control"
                                placeholder="Post Title"
                                value={title}
                                required
                                onChange={(e) => setTitle(e.target.value)}
                            />
                        </div>

                        <div className="mb-3">
                            <label htmlFor="content" className="form-label">Content</label>
                            <textarea
                                id="content"
                                className="form-control"
                                placeholder="Post Content"
                                value={content}
                                required
                                onChange={(e) => setContent(e.target.value)}
                            />
                        </div>

                        <div className="mb-3">
                            <label htmlFor="authorId" className="form-label">Author ID</label>
                            <input
                                id="authorId"
                                type="text"
                                className="form-control"
                                placeholder="Your Author ID"
                                value={authorId}
                                required
                                onChange={(e) => setAuthorId(e.target.value)}
                            />
                        </div>

                        <button
                            type="submit"
                            className="btn btn-primary w-100"
                            disabled={isSubmitting}
                        >
                            {isSubmitting ? 'Submitting...' : 'Add Post'}
                        </button>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default PostForm;
